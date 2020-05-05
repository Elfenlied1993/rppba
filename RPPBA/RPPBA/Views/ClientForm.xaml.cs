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
using Microsoft.EntityFrameworkCore;
using RPPBA.Models;
using RPPBA.TableModels;

namespace RPPBA.Views
{
    /// <summary>
    /// Interaction logic for ClientForm.xaml
    /// </summary>
    public partial class ClientForm : Window
    {
        private MainForm mainForm;
        private OrganizationsModel orgModel;
        public ClientForm(OrganizationsModel model, MainForm mainForm)
        {
            InitializeComponent();
            Name.Text = model.Name;
            Bank.Text = model.Account;
            Address.Text = model.Address;
            Phone.Text = model.Phone;
            Contact.Text = model.Director;
            Discount.Visibility = Visibility.Visible;
            Discount.Text = model.Discount;
            DiscountLabel.Visibility = Visibility.Visible;
            AddClient.Content = "Сохранить";
            Title.Content = "Редактирование данных клиента";
            this.mainForm = mainForm;
            orgModel = model;
        }
        public ClientForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void AddClient_OnClick(object sender, RoutedEventArgs e)
        {
            using (var entities = new RPPBAContext())
            {
                var address = Address.Text.Split(", ");
                //Country
                //Region
                //City
                //Street
                //House
                if (AddClient.Content == "Сохранить")
                {
                    var entity = entities.Organizations.Include("OrganizationAddress")
                        .Include("OrganizationAddress.City")
                        .Include("OrganizationAddress.City.Country")
                        .Include("OrganizationAddress.City.Country.Region")
                        .Include("Balance")
                        .Include("Discounts").FirstOrDefault(x => x.OrganizationId == orgModel.Id);
                    entity.OrganizationName = Name.Text;
                    entity.OrganizationPaymentAccount = Bank.Text;
                    entity.OrganizationDirectorFullname = Contact.Text;
                    entity.OrganizationPhoneInt = Phone.Text;
                    entity.OrganizationAddress.StreetName = address[3];
                    entity.OrganizationAddress.BuildingInt = address[4];
                    entity.OrganizationAddress.City.CityName = address[2];
                    entity.OrganizationAddress.City.Country.CountryName = address[0];
                    entity.OrganizationAddress.City.Country.Region.RegionName = address[1];
                    entity.Discounts.Discount = Convert.ToInt32(Discount.Text);
                    entities.SaveChanges();
                }
                else
                {
                    var model = new Organizations()
                    {
                        OrganizationName = Name.Text,
                        OrganizationPaymentAccount = Bank.Text,
                        OrganizationDirectorFullname = Contact.Text,
                        OrganizationPhoneInt = Phone.Text,
                        OrganizationAddress = new Addresses()
                        {
                            StreetName = address[3],
                            BuildingInt = address[4],
                            City = new Cities()
                            {
                                CityName = address[2],
                                Country = new Countries()
                                {
                                    CountryName = address[0],
                                    Region = new Regions()
                                    {
                                        RegionName = address[1]
                                    }
                                }
                            }
                        },
                    };
                    entities.Organizations.Add(model);
                    entities.SaveChanges();
                    model = entities.Organizations.FirstOrDefault(x =>
                        x.OrganizationName == model.OrganizationName &&
                        x.OrganizationPaymentAccount == model.OrganizationPaymentAccount);
                    model.Discounts= new Discounts()
                    {
                        Discount = 0
                    };
                    model.Balance = new Balance()
                    {
                        CurrentBalance = 0
                    };
                    entities.SaveChanges();
                }
            }
            this.Close();
            mainForm.LoadData();
            mainForm.UpdateView();
        }
    }
}
