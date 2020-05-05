using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RPPBA.Annotations;

namespace RPPBA.TableModels
{
    public partial class OrderProduct : INotifyPropertyChanged
    {
        private int _number;
        private int _id;
        private string _name;
        private decimal _cost;
        private int _quantity;
        private int _extraDiscount;
        private int _discount;
      

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

        public decimal Cost
        {
            get => _cost;
            set
            {
                if (value == _cost) return;
                _cost = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
    }
}
