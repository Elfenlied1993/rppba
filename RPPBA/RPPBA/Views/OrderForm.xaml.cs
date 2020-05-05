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
using System.Windows.Shapes;
using RPPBA.TableModels;

namespace RPPBA.Views
{
    /// <summary>
    /// Interaction logic for OrderForm.xaml
    /// </summary>
    public partial class OrderForm : Window
    {
        private MainForm mainForm;
        public OrderForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }
        public OrderForm(OrdersModel model, MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }
        private void AddOrder_OnClick(object sender, RoutedEventArgs e)
        {
            //Статус - в обработке
            throw new NotImplementedException();
        }

        private void AddProduct_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
