using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using RPPBA.Annotations;

namespace RPPBA.TableModels
{
    public partial class OrdersModel : INotifyPropertyChanged
    {
        private int _id;
        private int _number;
        private string _client;
        private decimal _sum;
        private DateTime _date;
        private string _address;
        private string _status;
        private string _comment;
        private string _phone;

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

        public string Client
        {
            get => _client;
            set
            {
                if (value == _client) return;
                _client = value;
                OnPropertyChanged();
            }
        }

        public decimal Sum
        {
            get => _sum;
            set
            {
                if (value == _sum) return;
                _sum = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                if (value.Equals(_date)) return;
                _date = value;
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

        public string Status
        {
            get => _status;
            set
            {
                if (value == _status) return;
                _status = value;
                OnPropertyChanged();
            }
        }

        public string Comment
        {
            get => _comment;
            set
            {
                if (value == _comment) return;
                _comment = value;
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
