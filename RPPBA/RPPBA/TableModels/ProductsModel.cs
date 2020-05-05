using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RPPBA.Annotations;

namespace RPPBA.TableModels
{
    public partial class ProductsModel : INotifyPropertyChanged
    {
        private int _number;
        private decimal _cost;
        private string _name;
        private int _id;
        private int _completed;
        private int _sold;
        private int _left;
        private int _reserved;
        private string _description;

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

        public int Completed
        {
            get => _completed;
            set
            {
                if (value == _completed) return;
                _completed = value;
                OnPropertyChanged();
            }
        }

        public int Sold
        {
            get => _sold;
            set
            {
                if (value == _sold) return;
                _sold = value;
                OnPropertyChanged();
            }
        }

        public int Left
        {
            get => _left;
            set
            {
                if (value == _left) return;
                _left = value;
                OnPropertyChanged();
            }
        }

        public int Reserved
        {
            get => _reserved;
            set
            {
                if (value == _reserved) return;
                _reserved = value;
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
