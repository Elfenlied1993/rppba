using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RPPBA.Models;
using RPPBA.TableModels;

namespace RPPBA.Views
{
    /// <summary>
    /// Interaction logic for ProductForm.xaml
    /// </summary>
    public partial class ProductForm : Window
    {
        private ProductsModel prModel;
        private MainForm mainForm;
        public ProductForm(ProductsModel model, MainForm mainForm)
        {
            InitializeComponent();
            ProductCost.Text = model.Cost.ToString();
            ProductDescription.Text = model.Description;
            ProductName.Text = model.Name;
            CreateProduct.Content = "Сохранить";
            Title.Content = "Редактирование данных товара";
            prModel = model;
            this.mainForm = mainForm;
        }
        public ProductForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void CreateProduct_OnClick(object sender, RoutedEventArgs e)
        {
            using (var entities = new RPPBAContext())
            {
                if (CreateProduct.Content == "Сохранить")
                {
                    var entity = entities.Products.FirstOrDefault(x => x.ProductId == prModel.Id);
                    entity.ProductPrice = Convert.ToInt32(ProductCost.Text);
                    entity.ProductAvailableQuantity += Convert.ToInt32(ProductCreated.Text);
                    entity.ProductName = ProductName.Text;
                    entity.ProductDescription = ProductDescription.Text;
                    entities.SaveChanges();
                }
                else
                {
                    entities.Products.Add(new Products()
                    {
                        ProductName = ProductName.Text,
                        ProductDescription = ProductDescription.Text,
                        ProductPrice = Convert.ToInt32(ProductCost.Text),
                        ProductAvailableQuantity = Convert.ToInt32(ProductCreated.Text),
                        ProductReservedQuantity = 0,
                        ProductSoldQuantity = 0
                    });
                    entities.SaveChanges();
                }
            }
            this.Close();
            mainForm.LoadData();
            mainForm.UpdateView();
        }
    }
}
