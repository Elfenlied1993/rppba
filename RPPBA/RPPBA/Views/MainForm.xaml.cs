using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RPPBA.Views
{
    /// <summary>
    /// Interaction logic for MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {
        private Models.System user;
        public MainForm(Models.System user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void LoadData()
        {
            
        }

        private void CreateOrder_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FilterOrders_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RefreshOrders_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SearchOrders_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Orders_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OrdersEdit_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OrdersBack_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OrderApprove_Clicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OrderDecline_Clicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OrderProductReserve_Clicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CreateClient_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RefreshClients_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SearchClients_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DeleteClient_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CreateProduct_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UpdateCost_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UpdateCount_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FilterProducts_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SearchProducts_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EditProduct_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DeleteProduct_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DiscountDelete_Clicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Logout_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
