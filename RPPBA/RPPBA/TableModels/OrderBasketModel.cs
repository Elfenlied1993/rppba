using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RPPBA.Annotations;

namespace RPPBA.TableModels
{
    public partial class OrderBasketModel : INotifyPropertyChanged
    {
        private int _transactionId;
        private int _orderId;
        private int _totalSale;
        private int _productId;
        private int _quantity;

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

        public int ProductId
        {
            get => _productId;
            set
            {
                if (value == _productId) return;
                _productId = value;
                OnPropertyChanged();
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value == _quantity) return;
                _quantity = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
