using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RPPBA.Models;
using RPPBA.Views;

namespace RPPBA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            using (var entities = new RPPBAContext())
            {
                if (entities.System.Any(x => x.Login.ToLower().Equals(Login.Text.ToLower()) && x.Password.Equals(Password.Password)))
                {
                    MainForm mainForm = new MainForm(entities.System.First(x=>x.Login.ToLower().Equals(Login.Text.ToLower())));
                    mainForm.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка входа. Проверьте введенные данные");
                }
            }
        }
    }
}
