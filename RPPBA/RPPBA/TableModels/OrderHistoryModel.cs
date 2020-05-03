using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RPPBA.Annotations;

namespace RPPBA.TableModels
{
    public partial class OrderHistoryModel : INotifyPropertyChanged
    {
        private int _historyId;
        private int _orderId;
        private Status _orderStatus;
        private DateTime _updateTime;
        private int _amountTransfer;
        private string _comment;

        public int HistoryId
        {
            get => _historyId;
            set
            {
                if (value == _historyId) return;
                _historyId = value;
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

        public Status OrderStatus
        {
            get => _orderStatus;
            set
            {
                if (value == _orderStatus) return;
                _orderStatus = value;
                OnPropertyChanged();
            }
        }

        public DateTime UpdateTime
        {
            get => _updateTime;
            set
            {
                if (value.Equals(_updateTime)) return;
                _updateTime = value;
                OnPropertyChanged();
            }
        }

        public int AmountTransfer
        {
            get => _amountTransfer;
            set
            {
                if (value == _amountTransfer) return;
                _amountTransfer = value;
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
