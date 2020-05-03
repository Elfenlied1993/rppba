using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RPPBA.Annotations;

namespace RPPBA.TableModels
{
    public partial class OrganizationsModel : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _director;
        private string _payment;
        private string _phone;
        private int _addressId;

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

        public string Payment
        {
            get => _payment;
            set
            {
                if (value == _payment) return;
                _payment = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

       [NotifyPropertyChangedInvocator]
       protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
       {
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
       }
    }
}
