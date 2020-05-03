using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RPPBA.Annotations;

namespace RPPBA.TableModels
{
    public partial class OrdersModel : INotifyPropertyChanged
    {
        private int _transactionId;
        private int _orderId;
        private int _organizationId;
        private int _addressId;
        private DateTime _shippingDate;
        private Status _status;
        private int _totalSale;
        private int _discount;
        private int _extraDiscount;
        private string _comment;
        public List<int> OrderBasketsId;
        public int TransactionId
        {
            get => _transactionId;
            set
            {
                if (value == _transactionId) return;
                _transactionId = value;
                OnPropertyChanged();
            }
        }

        public int OrderId
        {
            get => _orderId;
            set
            {
                if (value == _orderId) return;
                _orderId = value;
                OnPropertyChanged();
            }
        }

        public int OrganizationId
        {
            get => _organizationId;
            set
            {
                if (value == _organizationId) return;
                _organizationId = value;
                OnPropertyChanged();
            }
        }

        public int AddressId
        {
            get => _addressId;
            set
            {
                if (value == _addressId) return;
                _addressId = value;
                OnPropertyChanged();
            }
        }

        public DateTime ShippingDate
        {
            get => _shippingDate;
            set
            {
                if (value.Equals(_shippingDate)) return;
                _shippingDate = value;
                OnPropertyChanged();
            }
        }

        public Status Status
        {
            get => _status;
            set
            {
                if (value == _status) return;
                _status = value;
                OnPropertyChanged();
            }
        }

        public int TotalSale
        {
            get => _totalSale;
            set
            {
                if (value == _totalSale) return;
                _totalSale = value;
                OnPropertyChanged();
            }
        }

        public int Discount
        {
            get => _discount;
            set
            {
                if (value == _discount) return;
                _discount = value;
                OnPropertyChanged();
            }
        }

        public int ExtraDiscount
        {
            get => _extraDiscount;
            set
            {
                if (value == _extraDiscount) return;
                _extraDiscount = value;
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

    public enum Status
    {

    }
}
