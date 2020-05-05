using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RPPBA.Annotations;

namespace RPPBA.TableModels
{
    public partial class OrganizationsModel : INotifyPropertyChanged
    {
        private decimal _balance;
        private string _discount;
        private string _address;
        private string _director;
        private string _phone;
        private string _account;
        private int _number;
        private int _id;
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public int Id
        {
            get => _id;
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public int Number
        {
            get => _number;
            set
            {
                if (value == _number) return;
                _number = value;
                OnPropertyChanged();
            }
        }

        public string Account
        {
            get => _account;
            set
            {
                if (value == _account) return;
                _account = value;
                OnPropertyChanged();
            }
        }

        public string Director
        {
            get => _director;
            set
            {
                if (value == _director) return;
                _director = value;
                OnPropertyChanged();
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                if (value == _phone) return;
                _phone = value;
                OnPropertyChanged();
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                if (value == _address) return;
                _address = value;
                OnPropertyChanged();
            }
        }

        public string Discount
        {
            get => _discount;
            set
            {
                if (value == _discount) return;
                _discount = value;
                OnPropertyChanged();
            }
        }

        public decimal Balance
        {
            get => _balance;
            set
            {
                if (value == _balance) return;
                _balance = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

       [NotifyPropertyChangedInvocator]
       protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
       {
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
       }
    }
}
