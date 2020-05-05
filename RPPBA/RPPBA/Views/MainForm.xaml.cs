using Microsoft.EntityFrameworkCore;
using RPPBA.Models;
using RPPBA.TableModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using Constants = RPPBA.Services.Constants;

namespace RPPBA.Views
{
    /// <summary>
    /// Interaction logic for MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {

        private ObservableCollection<OrdersModel> _orders;
        private ObservableCollection<ProductsModel> _products;
        private ObservableCollection<OrganizationsModel> _organizations;
        private ObservableCollection<DiscountProgramModel> _discountPrograms;
        private ObservableCollection<OrderHistoryModel> _orderHistories;
        private ObservableCollection<OrderProduct> _orderProducts;
        private Models.System user;
        private ObservableCollection<OrdersModel> _organizationsOrders;
        private List<DiscountProgramModel> removingDiscountPrograms = new List<DiscountProgramModel>();
        public MainForm(Models.System user)
        {
            InitializeComponent();
            this.user = user;
            LoadComponents();
            LoadData();
        }

        public void LoadData()
        {
            _orders = new ObservableCollection<OrdersModel>();
            _products = new ObservableCollection<ProductsModel>();
            _organizations = new ObservableCollection<OrganizationsModel>();
            _discountPrograms = new ObservableCollection<DiscountProgramModel>();
            _orderHistories = new ObservableCollection<OrderHistoryModel>();
            _orderProducts = new ObservableCollection<OrderProduct>();
            _organizationsOrders = new ObservableCollection<OrdersModel>();
            using (var entities = new RPPBAContext())
            {
                if (entities.Orders != null)
                {

                    var orders = entities.Orders
                        .Include("OrderBasketOrder")
                        .Include("Organization")
                        .Include("Address")
                        .Include("OrderHistory")
                        .Include("Address.City")
                        .Include("Address.City.Country")
                        .Include("Address.City.Country.Region")
                        .AsEnumerable().ToList();
                    int i = 1;
                    foreach (var order in orders)
                    {
                        _orders.Add(new OrdersModel()
                        {
                            Address = order.Address.City.Country.CountryName + ", " + order.Address.City.Country.Region.RegionName +
                                      ", " + order.Address.City.CityName + ", " + order.Address.StreetName + ", " + order.Address.BuildingInt,
                            Status = order.OrderStatus,
                            Number = i,
                            Sum = (decimal)order.TotalSale,
                            Client = order.Organization.OrganizationName,
                            Comment = order.Comment,
                            Date = (DateTime)order.ShippingDate,
                            Id = order.OrderId,
                            Phone = order.Organization.OrganizationPhoneInt
                        });
                        i++;
                    }
                }

                if (entities.Products != null)
                {
                    var products = entities.Products.AsEnumerable().ToList();
                    int i = 1;
                    foreach (var product in products)
                    {
                        _products.Add(new ProductsModel()
                        {
                            Id = (int)product.ProductId,
                            Cost = (decimal)product.ProductPrice,
                            Left = (int)product.ProductAvailableQuantity,
                            Number = i,
                            Reserved = (int)product.ProductReservedQuantity,
                            Name = product.ProductName,
                            Description = product.ProductDescription,
                            Sold = (int)product.ProductSoldQuantity
                        });
                        i++;
                    }
                }

                if (entities.Organizations != null)
                {
                    var organizations = entities.Organizations.Include("Balance")
                        .Include("OrganizationAddress")
                        .Include("OrganizationAddress.City")
                        .Include("OrganizationAddress.City.Country")
                        .Include("OrganizationAddress.City.Country.Region")
                        .Include("Discounts").AsEnumerable().ToList();
                    int i = 1;
                    foreach (var organization in organizations)
                    {
                        _organizations.Add(new OrganizationsModel()
                        {
                            Id = (int)organization.OrganizationId,
                            Account = organization.OrganizationPaymentAccount,
                            Address = organization.OrganizationAddress.City.Country.CountryName + ", " + organization.OrganizationAddress.City.Country.Region.RegionName +
                                      ", " + organization.OrganizationAddress.City.CityName + ", " + organization.OrganizationAddress.StreetName + ", " + organization.OrganizationAddress.BuildingInt,
                            Balance = (decimal)organization.Balance.CurrentBalance,
                            Director = organization.OrganizationDirectorFullname,
                            Discount = organization.Discounts.Discount.ToString(),
                            Number = i,
                            Name = organization.OrganizationName,
                            Phone = organization.OrganizationPhoneInt
                        });
                        i++;
                    }
                }

                if (entities.DiscountPrograms != null)
                {
                    var discountPrograms = entities.DiscountPrograms.AsEnumerable().ToList();
                    int i = 1;
                    foreach (var discountProgram in discountPrograms)
                    {
                        _discountPrograms.Add(new DiscountProgramModel()
                        {
                            Discount = (int)discountProgram.Discount,
                            Begin = (decimal)discountProgram.StartSum,
                            End = (decimal)discountProgram.EndSum,
                            Id = (int)discountProgram.DiscountId
                        });
                        i++;
                    }
                }

                Orders.ItemsSource = _orders;
                Products.ItemsSource = _products;
                Clients.ItemsSource = _organizations;
                DiscountPrograms.ItemsSource = _discountPrograms;
                DiscountProgramsEditable.ItemsSource = _discountPrograms;
            }
        }
        private void LoadComponents()
        {
            if (user.Role == 1)
            {
                OrdersApprove.Width = 75;
                OrdersDecline.Width = 75;
                OrdersCommentary.Width = 125;
                OrdersAddress.Width = 135;
                OrderProductsName.Width = 240;
                OrderProductsReserve.Width = 160;
            }
            else
            {
                OrdersApprove.Width = 0;
                OrdersDecline.Width = 0;
                OrderProductsReserve.Width = 0;

            }
        }
        private void CreateOrder_OnClick(object sender, RoutedEventArgs e)
        {
            if (CreateOrder.Content == "Зарезервировать")
            {
                //Если одобрен
                //Если меньше количества оставшихся -> alert
                //Добавляется к зарезервировано, а из остатка -зарезервированные
                var model = (OrdersModel)Orders.SelectedItem;
                if (model.Status != Constants.APPROVED)
                    return;
                using (var entities = new RPPBAContext())
                {
                    var entity = entities.Orders.Include("OrderBasketOrder")
                        .Include("OrderBasketOrder.Product")
                        .Include("Organization")
                        .Include("Address")
                        .Include("OrderHistory")
                        .Include("Address.City")
                        .Include("Address.City.Country")
                        .Include("Address.City.Country.Region").FirstOrDefault(x => x.TransactionId == model.Id);

                    foreach (var orderBasket in entity.OrderBasketOrder)
                    {
                        if (orderBasket.Product.ProductAvailableQuantity < orderBasket.Quantity)
                        {
                            var messageString = "Указанное количество " + orderBasket.Product.ProductName + " недоступно для заказа. Вы можете зарезервировать " + orderBasket.Product.ProductAvailableQuantity + " " + orderBasket.Product.ProductName + "";
                            MessageBox.Show(messageString);
                            return;
                        }
                        orderBasket.Product.ProductReservedQuantity += orderBasket.Quantity;
                        orderBasket.Product.ProductAvailableQuantity -= orderBasket.Quantity;
                    }
                    entity.Organization.Balance.CurrentBalance += entity.TotalSale;
                    entities.OrderHistory.Add(new OrderHistory()
                    {
                        OrderId = entity.OrderId,
                        AmountTransfer = entity.TotalSale,
                        Comment = "",
                        Order = entity,
                        OrderStatus = Constants.RESERVED,
                        StatusUpdateDate = DateTime.Now
                    });
                    entity.OrderStatus = model.Status;
                    model.Status = Constants.RESERVED;
                    entities.SaveChanges();
                    Status.Content = "Статус: " + Constants.RESERVED;
                    LoadData();
                }
            }
            else
            {
                OrderForm orderForm = new OrderForm(this);
                orderForm.Show();
                LoadData();
            }
        }

        private void FilterOrders_OnClick(object sender, RoutedEventArgs e)
        {
            FilterOrders filterOrders = new FilterOrders(this);
            filterOrders.Show();
            LoadData();
        }

        private void RefreshOrders_OnClick(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SearchOrders_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchOrders.Text != "Поиск")
            {
                try
                {
                    var searched = _orders.Where(x =>
                        x.Client.Contains(SearchOrders.Text)).ToList();
                    Orders.ItemsSource = searched;
                    var searchedId = _orders.Where(x =>
                        x.Id == Convert.ToInt32(SearchOrders.Text)).ToList();
                    foreach (var searchedIdModel in searchedId)
                    {
                        searched.Add(searchedIdModel);
                    }
                    Orders.ItemsSource = searched;

                }
                catch
                {

                }
            }
            if (SearchOrders.Text == "")
            {
                LoadData();
            }
        }

        private void Orders_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Orders.SelectedItem == null)
            {
                return;
            }
            else
            {
                var selectedOrder = (OrdersModel)Orders.SelectedItem;
                ParticularOrderButtons.Visibility = Visibility.Visible;
                ParticularOrder.Visibility = Visibility.Visible;
                CreateOrder.Content = "Зарезервировать";
                OrderID.Content = "Заказ №" + selectedOrder.Id.ToString();
                ClientName.Content = "" + selectedOrder.Client;
                DepartureDate.Content = "Дата отгрузки: " + selectedOrder.Date.ToString();
                Sum.Content = "Сумма: " + (selectedOrder.Sum > 0 ? "+" : "") + selectedOrder.Sum.ToString();
                Status.Content = "Статус: " + selectedOrder.Status;
                Commentary.Content = "Комментарий: " + selectedOrder.Comment;
                Address.Content = "Адрес: " + selectedOrder.Address;
                PhoneNumber.Content = "Телефон: " + selectedOrder.Phone;
                _orderProducts = new ObservableCollection<OrderProduct>();
                _orderHistories = new ObservableCollection<OrderHistoryModel>();
                using (var entities = new RPPBAContext())
                {
                    var order = entities.Orders.Include("OrderBasketOrder")
                        .Include("OrderBasketOrder.Product")
                        .Include("Organization")
                        .Include("Address")
                        .Include("OrderHistory")
                        .Include("Address.City")
                        .Include("Address.City.Country")
                        .Include("Address.City.Country.Region").First(x => x.OrderId == selectedOrder.Id);
                    int i = 1;
                    foreach (var product in order.OrderBasketOrder)
                    {
                        _orderProducts.Add(new OrderProduct()
                        {
                            Cost = (decimal)product.Product.ProductPrice,
                            ExtraDiscount = (int)product.Order.ExtraDiscount,
                            Discount = (int)product.Order.Discount,
                            Id = product.ProductId,
                            Name = product.Product.ProductName,
                            Number = i,
                            Quantity = (int)product.Quantity
                        });
                        i++;
                    }

                    i = 1;
                    foreach (var history in order.OrderHistory)
                    {
                        _orderHistories.Add(new OrderHistoryModel()
                        {
                            Comment = history.Comment,
                            Date = (DateTime)history.StatusUpdateDate,
                            Id = (int)history.OrderHistoryId,
                            Number = i,
                            Status = history.OrderStatus,
                            Sum = (selectedOrder.Sum > 0 ? "+" : "") + (decimal)history.AmountTransfer
                        });
                        i++;
                    }

                    OrderProducts.ItemsSource = _orderProducts;
                    History.ItemsSource = _orderHistories;
                }

            }

        }

        private void OrdersEdit_OnClick(object sender, RoutedEventArgs e)
        {
            OrderForm orderForm = new OrderForm((OrdersModel)Orders.SelectedItem, this);
            orderForm.Show();
            LoadData();
        }

        private void OrdersBack_OnClick(object sender, RoutedEventArgs e)
        {
            using (var entities = new RPPBAContext())
            {
                var selected = entities.Orders.Include("OrderBasketOrder")
                    .Include("Organization")
                    .Include("OrderBasketOrder.Product")
                    .Include("Address")
                    .Include("OrderHistory")
                    .Include("Address.City")
                    .Include("Address.City.Country")
                    .Include("Address.City.Country.Region").FirstOrDefault(x => x.OrderId == Convert.ToInt32(OrderID.Content.ToString().Replace("Заказ №", "")));
                if (selected.OrderStatus == Constants.SHIPPED_APPROVED)
                    return;
                if (selected.OrderStatus == Constants.RESERVED || selected.OrderStatus == Constants.PAYED)
                {
                    foreach (var orderBasket in selected.OrderBasketOrder)
                    {
                        orderBasket.Product.ProductReservedQuantity -= orderBasket.Quantity;
                        orderBasket.Product.ProductAvailableQuantity += orderBasket.Quantity;
                    }
                }
                selected.Organization.Balance.CurrentBalance -= selected.TotalSale;
                entities.OrderHistory.Add(new OrderHistory()
                {
                    OrderId = selected.OrderId,
                    AmountTransfer = selected.OrderStatus == Constants.RESERVED ? -selected.TotalSale : 0,
                    Comment = "",
                    Order = selected,
                    OrderStatus = Constants.SHIPPED_DECLINED,
                    StatusUpdateDate = DateTime.Now
                });
                Status.Content = "Статус: " + Constants.SHIPPED_DECLINED;
                selected.OrderStatus = Constants.SHIPPED_DECLINED;
                entities.SaveChanges();
                LoadData();
            }
        }

        private void OrderApprove_Clicked(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var selectedOrder = (OrdersModel)button.DataContext;
            if (selectedOrder.Status != Constants.HANDLING && selectedOrder.Status != Constants.DECLINED)
                return;
            selectedOrder.Status = Constants.APPROVED;
            using (var entities = new RPPBAContext())
            {
                var entity = entities.Orders.FirstOrDefault(x => x.OrderId == selectedOrder.Id);
                entity.OrderStatus = Constants.APPROVED;
                entities.OrderHistory.Add(new OrderHistory()
                {
                    OrderId = entity.OrderId,
                    AmountTransfer = 0,
                    Comment = "",
                    Order = entity,
                    OrderStatus = Constants.APPROVED,
                    StatusUpdateDate = DateTime.Now
                });
                entities.SaveChanges();
            }
        }

        private void OrderDecline_Clicked(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException(); 
            //Отменён -> В обработке
            var button = (Button)sender;
            var selectedOrder = (OrdersModel)button.DataContext;
            if (selectedOrder.Status != Constants.HANDLING)
                return;
            selectedOrder.Status = Constants.DECLINED;
            using (var entities = new RPPBAContext())
            {
                var entity = entities.Orders.FirstOrDefault(x => x.OrderId == selectedOrder.Id);
                entity.OrderStatus = Constants.DECLINED;
                entities.OrderHistory.Add(new OrderHistory()
                {
                    OrderId = entity.OrderId,
                    AmountTransfer = 0,
                    Comment = "",
                    Order = entity,
                    OrderStatus = Constants.DECLINED,
                    StatusUpdateDate = DateTime.Now
                });
                entities.SaveChanges();
            }
        }

        private void OrderProductReserve_Clicked(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var selectedProduct = (ProductsModel)button.DataContext;
            using (var entities = new RPPBAContext())
            {
                var selected = entities.Orders.Include("OrderBasketOrder")
                    .Include("Organization")
                    .Include("OrderBasketOrder.Product")
                    .Include("Address")
                    .Include("OrderHistory")
                    .Include("Address.City")
                    .Include("Address.City.Country")
                    .Include("Address.City.Country.Region").FirstOrDefault(x => x.OrderId == Convert.ToInt32(OrderID.Content.ToString().Replace("Заказ №", "")));
                if (selected.OrderStatus != Constants.RESERVED)
                {
                    return;
                }
                var selectedProductEntity = entities.Products.First(x => x.ProductId == selectedProduct.Id);
                selectedProductEntity.ProductAvailableQuantity += selected.OrderBasketOrder
                    .FirstOrDefault(x => x.ProductId == selectedProductEntity.ProductId).Quantity;
                selectedProductEntity.ProductReservedQuantity -= selected.OrderBasketOrder
                    .FirstOrDefault(x => x.ProductId == selectedProductEntity.ProductId).Quantity;
                selected.Organization.Balance.CurrentBalance -= selected.TotalSale;
                entities.OrderHistory.Add(new OrderHistory()
                {
                    OrderId = selected.OrderId,
                    AmountTransfer = -selected.TotalSale,
                    Comment = "",
                    Order = selected,
                    OrderStatus = Constants.APPROVED,
                    StatusUpdateDate = DateTime.Now
                });
                selected.OrderStatus = Constants.APPROVED;
                entities.SaveChanges();
            }
        }

        private void CreateClient_OnClick(object sender, RoutedEventArgs e)
        {
            if (CreateClient.Content == "Редактировать данные")
            {
                ClientForm clientForm = new ClientForm((OrganizationsModel)Clients.SelectedItem, this);
                clientForm.Show();
                LoadData();
            }
            else
            {
                ClientForm clientForm = new ClientForm(this);
                clientForm.Show();
                LoadData();
            }
        }

        private void RefreshClients_OnClick(object sender, RoutedEventArgs e)
        {
            LoadData();

        }

        private void SearchClients_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchClients.Text != "Поиск")
            {
                try
                {
                    var searched = _organizations.Where(x => x.Name.Contains(SearchClients.Text) || x.Address.Contains(SearchClients.Text) || x.Director.Contains(SearchClients.Text) || x.Account.Contains(SearchClients.Text) || x.Phone.Contains(SearchClients.Text)).ToList();
                    Clients.ItemsSource = searched;

                }
                catch
                {

                }
            }
            if (SearchClients.Text == "")
            {
                LoadData();
            }

        }

        private void DeleteClient_OnClick(object sender, RoutedEventArgs e)
        {
            var model = (OrganizationsModel)Clients.SelectedItem;
            using (var entities = new RPPBAContext())
            {
                var client = entities.Organizations.Include("Balance").Include("Discounts").FirstOrDefault(x => x.OrganizationId == model.Id);
                entities.Balance.Remove(client.Balance);
                entities.Discounts.Remove(client.Discounts);
                foreach (var order in entities.Orders.Include("OrderBasketOrder")
                    .Include("Organization")
                    .Include("OrderBasketOrder.Product")
                    .Include("Address")
                    .Include("OrderHistory")
                    .Include("Address.City")
                    .Include("Address.City.Country")
                    .Include("Address.City.Country.Region").Where(x => x.OrganizationId == client.OrganizationId))
                {
                    foreach (var orderHistory in order.OrderHistory)
                    {
                        entities.OrderHistory.Remove(orderHistory);
                    }
                    foreach (var orderBasket in order.OrderBasketOrder)
                    {
                        if (order.OrderStatus == Constants.RESERVED)
                        {
                            orderBasket.Product.ProductReservedQuantity -= orderBasket.Quantity;
                            orderBasket.Product.ProductAvailableQuantity += orderBasket.Quantity;
                        }
                        entities.OrderBasket.Remove(orderBasket);
                    }

                    entities.Orders.Remove(order);
                }
                entities.Organizations.Remove(client);
                entities.SaveChanges();
                UpdateView();
            }
        }

        private void CreateProduct_OnClick(object sender, RoutedEventArgs e)
        {
            ProductForm productForm = new ProductForm(this);
            productForm.Show();
            LoadData();
        }

        private void UpdateCost_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var cost = File.ReadAllText(openFileDialog.FileName).Split("\r\n");
                foreach (var s in cost)
                {
                    var costs = s.Split(" ");
                    using (var entities = new RPPBAContext())
                    {
                        var entity = entities.Products.FirstOrDefault(x => x.ProductId == Convert.ToInt32(costs[0]));
                        entity.ProductPrice = Convert.ToInt32(costs[1]);
                        entities.SaveChanges();
                    }
                }
                LoadData();
            }
        }

        private void UpdateCount_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var cost = File.ReadAllText(openFileDialog.FileName).Split("\r\n");
                foreach (var s in cost)
                {
                    var costs = s.Split(" ");
                    using (var entities = new RPPBAContext())
                    {
                        var entity = entities.Products.FirstOrDefault(x => x.ProductId == Convert.ToInt32(costs[0]));
                        entity.ProductAvailableQuantity += Convert.ToInt32(costs[1]);
                        var productsModel = _products.FirstOrDefault(x => x.Id == Convert.ToInt32(costs[0]));
                        productsModel.Completed = Convert.ToInt32(costs[1]);
                        productsModel.Left += Convert.ToInt32(costs[1]);
                        entities.SaveChanges();
                    }
                }
            }
        }

        private void FilterProducts_OnClick(object sender, RoutedEventArgs e)
        {
            FilterProducts filterProducts = new FilterProducts(this);
            filterProducts.Show();
            LoadData();
        }

        private void SearchProducts_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchProducts.Text != "Поиск")
            {
                {
                    try
                    {
                        var searched = _products.Where(x =>
                            x.Name.Contains(SearchProducts.Text)).ToList();
                        Products.ItemsSource = searched;

                    }
                    catch
                    {

                    }
                }
                if (SearchProducts.Text == "")
                {
                    LoadData();
                }
            }
        }

        private void EditProduct_OnClick(object sender, RoutedEventArgs e)
        {
            ProductForm productForm = new ProductForm((ProductsModel)Products.SelectedItem, this);
            productForm.Show();
            LoadData();
        }

        private void DeleteProduct_OnClick(object sender, RoutedEventArgs e)
        {
            var model = (ProductsModel)Products.SelectedItem;
            using (var entities = new RPPBAContext())
            {
                var product = entities.Products.Include("OrderBasket")
                    .Include("OrderBasket.Order")
                    .Include("OrderBasket.Product")
                    .Include("OrderBasket.Transaction").FirstOrDefault(x => x.ProductId == model.Id);
                if (product.ProductReservedQuantity > 0)
                {
                    MessageBox.Show("Товар зарезервирован!");
                    return;
                }
                foreach (var orderBasket in entities.OrderBasket)
                {
                    if (orderBasket.ProductId == product.ProductId)
                    {
                        orderBasket.Order.TotalSale -= orderBasket.Quantity * orderBasket.Product.ProductPrice;
                        entities.OrderBasket.Remove(orderBasket);
                    }
                }
                entities.Products.Remove(product);
                entities.SaveChanges();
                UpdateView();
            }
        }

        private void DiscountDelete_Clicked(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var selectedDiscount = (DiscountProgramModel)button.DataContext;
            removingDiscountPrograms.Add(selectedDiscount);
            _discountPrograms.Remove(selectedDiscount);
        }

        private void Logout_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow authForm = new MainWindow();
            authForm.Show();
            this.Close();
        }

        private void Status_OnChanged(object sender, SelectionChangedEventArgs e)
        {
            //throw new NotImplementedException();
            //Статус отменён - всё равно остаётся
            //Зарезервировано меняется на оплачено, и сумма заказа добавляется к балансу
            //Отгружено - баланс с минусом кол-во в зарезервировано отменяется от значения в зарезерв. Сумма продано прибавляется на это число
            //В обработке -> отклонён
            //Всё кроме отгружено и отменён
            //Если зарезервирован - добавляется в текущий остаток, а от зарезерв. вычитается
            //Оплачен- добавляется в остаток, а от резерв. вычитается
            var comboBox = (ComboBox)sender;
            var selectedOrder = (OrdersModel)comboBox.DataContext;
            var item = (ComboBoxItem)e.AddedItems[0];
            var value = item.Content;
            if ((string)value == Constants.SHIPPED_DECLINED)
            {
                using (var entities = new RPPBAContext())
                {
                    var selected = entities.Orders.Include("OrderBasketOrder")
                        .Include("Organization")
                        .Include("OrderBasketOrder.Product")
                        .Include("Address")
                        .Include("OrderHistory")
                        .Include("Address.City")
                        .Include("Address.City.Country")
                        .Include("Address.City.Country.Region").FirstOrDefault(x => x.OrderId == selectedOrder.Id);
                    if (selected.OrderStatus == Constants.SHIPPED_APPROVED)
                        return;
                    if (selected.OrderStatus == Constants.RESERVED || selected.OrderStatus == Constants.PAYED)
                    {
                        foreach (var orderBasket in selected.OrderBasketOrder)
                        {
                            orderBasket.Product.ProductReservedQuantity -= orderBasket.Quantity;
                            orderBasket.Product.ProductAvailableQuantity += orderBasket.Quantity;
                        }
                    }
                    selected.Organization.Balance.CurrentBalance -= selected.TotalSale;

                    entities.OrderHistory.Add(new OrderHistory()
                    {
                        OrderId = selected.OrderId,
                        AmountTransfer = selected.OrderStatus == Constants.PAYED ? -selected.TotalSale : 0,
                        Comment = "",
                        Order = selected,
                        OrderStatus = Constants.SHIPPED_DECLINED,
                        StatusUpdateDate = DateTime.Now
                    });
                    selected.OrderStatus = Constants.SHIPPED_DECLINED;
                    selectedOrder.Status = Constants.SHIPPED_DECLINED;

                    entities.SaveChanges();
                    LoadData();
                }
            }
            if ((string)value == Constants.RESERVED)
            {
                using (var entities = new RPPBAContext())
                {
                    var entity = entities.Orders.Include("OrderBasketOrder")
                        .Include("OrderBasketOrder.Product")
                        .Include("Organization")
                        .Include("Address")
                        .Include("OrderHistory")
                        .Include("Address.City")
                        .Include("Address.City.Country")
                        .Include("Address.City.Country.Region").FirstOrDefault(x => x.TransactionId == selectedOrder.Id);
                    double totalSum = 0;
                    foreach (var orderBasket in entity.OrderBasketOrder)
                    {
                        if (orderBasket.Product.ProductAvailableQuantity < orderBasket.Quantity)
                        {
                            var messageString = "Указанное количество " + orderBasket.Product.ProductName + " недоступно для заказа. Вы можете зарезервировать " + orderBasket.Product.ProductAvailableQuantity + " " + orderBasket.Product.ProductName + "";
                            MessageBox.Show(messageString);
                            return;
                        }
                        orderBasket.Product.ProductReservedQuantity += orderBasket.Quantity;
                        orderBasket.Product.ProductAvailableQuantity -= orderBasket.Quantity;
                    }
                    entities.OrderHistory.Add(new OrderHistory()
                    {
                        OrderId = entity.OrderId,
                        AmountTransfer = entity.TotalSale,
                        Comment = "",
                        Order = entity,
                        OrderStatus = Constants.RESERVED,
                        StatusUpdateDate = DateTime.Now
                    });
                    entity.OrderStatus = Constants.RESERVED;
                    selectedOrder.Status = Constants.RESERVED;
                    entities.SaveChanges();
                    Status.Content = "Статус: " + Constants.RESERVED;
                    LoadData();
                }
            }
            if ((string)value == Constants.SHIPPED_APPROVED)
            {
                using (var entities = new RPPBAContext())
                {
                    var entity = entities.Orders.Include("OrderBasketOrder")
                        .Include("OrderBasketOrder.Product")
                        .Include("Organization")
                        .Include("Address")
                        .Include("OrderHistory")
                        .Include("Address.City")
                        .Include("Address.City.Country")
                        .Include("Address.City.Country.Region").FirstOrDefault(x => x.TransactionId == selectedOrder.Id);
                    if (selectedOrder.Status != Constants.PAYED)
                        return;
                    entity.Organization.Balance.CurrentBalance -= entity.TotalSale;
                    foreach (var orderBasket in entity.OrderBasketOrder)
                    {
                        orderBasket.Product.ProductReservedQuantity -= orderBasket.Quantity;
                        orderBasket.Product.ProductSoldQuantity += orderBasket.Quantity;
                    }
                    entities.OrderHistory.Add(new OrderHistory()
                    {
                        OrderId = entity.OrderId,
                        AmountTransfer = -entity.TotalSale,
                        Comment = "",
                        Order = entity,
                        OrderStatus = Constants.SHIPPED_APPROVED,
                        StatusUpdateDate = DateTime.Now
                    });
                    entity.OrderStatus = selectedOrder.Status;
                    selectedOrder.Status = Constants.SHIPPED_APPROVED;
                    entities.SaveChanges();
                    Status.Content = "Статус: " + Constants.SHIPPED_APPROVED;
                    LoadData();
                }
            }
            if ((string)value == Constants.PAYED)
            {
                using (var entities = new RPPBAContext())
                {
                    var entity = entities.Orders.Include("OrderBasketOrder")
                        .Include("OrderBasketOrder.Product")
                        .Include("Organization")
                        .Include("Address")
                        .Include("OrderHistory")
                        .Include("Address.City")
                        .Include("Address.City.Country")
                        .Include("Address.City.Country.Region").FirstOrDefault(x => x.TransactionId == selectedOrder.Id);
                    if (selectedOrder.Status != Constants.RESERVED)
                        return;
                    entity.Organization.Balance.CurrentBalance += entity.TotalSale;
                    foreach (var orderBasket in entity.OrderBasketOrder)
                    {
                        orderBasket.Product.ProductReservedQuantity -= orderBasket.Quantity;
                        orderBasket.Product.ProductAvailableQuantity += orderBasket.Quantity;
                    }
                    entities.OrderHistory.Add(new OrderHistory()
                    {
                        OrderId = entity.OrderId,
                        AmountTransfer = entity.TotalSale,
                        Comment = "",
                        Order = entity,
                        OrderStatus = Constants.PAYED,
                        StatusUpdateDate = DateTime.Now
                    });
                    entity.OrderStatus = selectedOrder.Status;
                    selectedOrder.Status = Constants.PAYED;
                    entities.SaveChanges();
                    Status.Content = "Статус: " + Constants.PAYED;
                    LoadData();
                }
            }
            if ((string)value == Constants.HANDLING)
            {
                using (var entities = new RPPBAContext())
                {
                    var selected = entities.Orders.Include("OrderBasketOrder")
                        .Include("Organization")
                        .Include("OrderBasketOrder.Product")
                        .Include("Address")
                        .Include("OrderHistory")
                        .Include("Address.City")
                        .Include("Address.City.Country")
                        .Include("Address.City.Country.Region").FirstOrDefault(x => x.OrderId == selectedOrder.Id);
                    if (selected.OrderStatus == Constants.SHIPPED_APPROVED)
                        return;
                    if (selected.OrderStatus == Constants.RESERVED || selected.OrderStatus == Constants.PAYED)
                    {
                        foreach (var orderBasket in selected.OrderBasketOrder)
                        {
                            orderBasket.Product.ProductReservedQuantity -= orderBasket.Quantity;
                            orderBasket.Product.ProductAvailableQuantity += orderBasket.Quantity;
                        }
                    }
                    selected.OrderStatus = Constants.HANDLING;
                    selectedOrder.Status = Constants.HANDLING;
                    entities.SaveChanges();
                    LoadData();
                }
            }
        }

        private void Clients_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Clients.SelectedItem == null)
            {
                return;
            }
            else
            {
                var selectedOrder = (OrganizationsModel)Clients.SelectedItem;
                ParticularClient.Visibility = Visibility.Visible;
                CreateClient.Content = "Редактировать данные";
                DeleteClient.Visibility = Visibility.Visible;
                ClientsName.Content = selectedOrder.Name;
                ClientsAddress.Content = "Адрес: " + selectedOrder.Address;
                ClientsBalance.Content = "Баланс: " + (selectedOrder.Balance > 0 ? "+" : "") + selectedOrder.Balance;
                ClientsBank.Content = "Счёт: " + selectedOrder.Account;
                ClientsDiscount.Content = "Скидка: " + selectedOrder.Discount + "%";
                ClientsPhone.Content = "Телефон: " + selectedOrder.Phone;
                ClientsContact.Content += "Представитель: " + selectedOrder.Director;
                _organizationsOrders = new ObservableCollection<OrdersModel>();
                using (var entities = new RPPBAContext())
                {
                    var client = entities.Organizations.Include("Orders")
                        .Include("Orders.OrderBasketOrder.Product")
                        .Include("Orders.Address")
                        .Include("Orders.OrderHistory")
                        .Include("Orders.Address.City")
                        .Include("Orders.Address.City.Country")
                        .Include("Orders.Address.City.Country.Region").First(x => x.OrganizationId == selectedOrder.Id);
                    int i = 1;
                    foreach (var order in client.Orders)
                    {
                        _organizationsOrders.Add(new OrdersModel()
                        {
                            Address = order.Address.City.Country.CountryName + ", " + order.Address.City.Country.Region.RegionName +
                                      ", г." + order.Address.City.CityName + ", ул." + order.Address.StreetName + ", д." + order.Address.BuildingInt,
                            Status = order.OrderStatus,
                            Number = i,
                            Sum = (decimal)order.TotalSale,
                            Client = order.Organization.OrganizationName,
                            Comment = order.Comment,
                            Date = (DateTime)order.ShippingDate,
                            Id = order.OrderId,
                            Phone = order.Organization.OrganizationPhoneInt
                        });
                        i++;
                    }

                    ClientOrders.ItemsSource = _organizationsOrders;
                }
            }
        }

        private void Products_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Products.SelectedItem == null)
            {
                return;
            }
            var selectedOrder = (ProductsModel)Products.SelectedItem;
            ProductButtons.Visibility = Visibility.Hidden;
            ParticularProduct.Visibility = Visibility.Visible;
            ProductCost.Content = "Цена:" + selectedOrder.Cost;
            ProductLeft.Content = selectedOrder.Left;
            ProductCreated.Content = selectedOrder.Completed;
            ProductReserved.Content = selectedOrder.Reserved;
            ProductDescription.Content = "Описание: " + selectedOrder.Description;
            ProductSold.Content = selectedOrder.Sold;
            ProductName.Content = selectedOrder.Name;
        }

        private void TabChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var control = (TabControl)e.OriginalSource;
                var selected = (TabItem)control.SelectedItem;
                if (control.Items.Count == 2)
                {
                    return;
                }

                if (((string)selected.Header == "Товары" || (string)selected.Header == "Заказы" ||
                     (string)selected.Header == "Клиенты" || (string)selected.Header == "Программа лояльности"))
                {
                    UpdateView();
                }
            }
            catch
            {
                return;
            }
        }
        public void UpdateView()
        {
            CreateClient.Content = "Добавить";
            DeleteClient.Visibility = Visibility.Hidden;
            ParticularOrder.Visibility = Visibility.Hidden;
            ParticularOrderButtons.Visibility = Visibility.Hidden;
            ParticularProduct.Visibility = Visibility.Hidden;
            ParticularClient.Visibility = Visibility.Hidden;
            CreateOrder.Content = "Создать заказ";
            ProductButtons.Visibility = Visibility.Visible;
            ParticularProduct.Visibility = Visibility.Hidden;
            AddDiscount.Visibility = Visibility.Hidden;
            DiscountProgramsEditable.Visibility = Visibility.Hidden;
            LoadData();
        }
        private void ChangeDiscount_OnClick(object sender, RoutedEventArgs e)
        {
            if (AddDiscount.Visibility != Visibility.Visible)
            {
                AddDiscount.Visibility = Visibility.Visible;
                DiscountProgramsEditable.Visibility = Visibility.Visible;
            }
            else
            {
                DiscountProgramsEditable.Visibility = Visibility.Hidden;

                AddDiscount.Visibility = Visibility.Hidden;
            }
        }

        private void SaveDiscounts_OnClick(object sender, RoutedEventArgs e)
        {
            using (var entities = new RPPBAContext())
            {
                foreach (var discountProgramModel in removingDiscountPrograms)
                {
                    if (discountProgramModel.Id != 0)
                    {
                        entities.DiscountPrograms.Remove(
                            entities.DiscountPrograms.First(x => x.DiscountId == discountProgramModel.Id));

                    }
                }

                foreach (var discountProgramModel in _discountPrograms)
                {
                    if (discountProgramModel.Id != 0)
                    {
                        var discountProgram = entities.DiscountPrograms.FirstOrDefault(x => x.DiscountId == discountProgramModel.Id);
                        discountProgram.Discount = discountProgramModel.Discount;
                        discountProgram.EndSum = discountProgramModel.End;
                        discountProgram.StartSum = discountProgramModel.Begin;
                        entities.SaveChanges();
                    }
                    else
                    {
                        var discountProgram = new DiscountProgram()
                        {
                            Discount = discountProgramModel.Discount,
                            EndSum = discountProgramModel.End,
                            StartSum = discountProgramModel.Begin
                        };
                        entities.Add(discountProgram);
                        entities.SaveChanges();
                    }
                }
                entities.SaveChanges();
                AddDiscount.Visibility = Visibility.Hidden;
                DiscountProgramsEditable.Visibility = Visibility.Hidden;
                LoadData();
            }
        }

        private void RefreshDiscounts_OnClick(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void AddDiscount_OnClick(object sender, RoutedEventArgs e)
        {
            _discountPrograms.Add(new DiscountProgramModel()
            {
                Begin = 0,
                Discount = 0,
                End = 0
            });
            DiscountPrograms.ItemsSource = null;
            DiscountPrograms.ItemsSource = _discountPrograms;
        }
        private void RefreshProducts_OnClick(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
