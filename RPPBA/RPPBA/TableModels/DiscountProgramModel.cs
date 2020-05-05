using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using RPPBA.Annotations;

namespace RPPBA.TableModels
{
    public class DiscountProgramModel : INotifyPropertyChanged
    {
        private int _discount;
        private decimal _end;
        private decimal _begin;
        private int _id;

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

        public decimal Begin
        {
            get => _begin;
            set
            {
                if (value == _begin) return;
                _begin = value;
                OnPropertyChanged();
            }
        }

        public decimal End
        {
            get => _end;
            set
            {
                if (value == _end) return;
                _end = value;
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
