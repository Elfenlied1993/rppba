using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RPPBA.Annotations;

namespace RPPBA.TableModels
{
    public partial class ProductsModel : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _description;
        private int _price;
        private int _availableQuantity;
        private int _reservedQuantity;

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

        public string Description
        {
            get => _description;
            set
            {
                if (value == _description) return;
                _description = value;
                OnPropertyChanged();
            }
        }

        public int Price
        {
            get => _price;
            set
            {
                if (value == _price) return;
                _price = value;
                OnPropertyChanged();
            }
        }

        public int AvailableQuantity
        {
            get => _availableQuantity;
            set
            {
                if (value == _availableQuantity) return;
                _availableQuantity = value;
                OnPropertyChanged();
            }
        }

        public int ReservedQuantity
        {
            get => _reservedQuantity;
            set
            {
                if (value == _reservedQuantity) return;
                _reservedQuantity = value;
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
