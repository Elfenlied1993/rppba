using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using RPPBA.Annotations;

namespace RPPBA.TableModels
{
    public class AddressesModel : INotifyPropertyChanged
    {
        private int _id;
        private int _cityId;
        private int _countryId;
        private int _regionId;
        private string _streetName;
        private string _buildingNumber;
        private string _cityName;
        private string _countryName;
        private string _regionName;

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

        public int CityId
        {
            get => _cityId;
            set
            {
                if (value == _cityId) return;
                _cityId = value;
                OnPropertyChanged();
            }
        }

        public int CountryId
        {
            get => _countryId;
            set
            {
                if (value == _countryId) return;
                _countryId = value;
                OnPropertyChanged();
            }
        }

        public int RegionId
        {
            get => _regionId;
            set
            {
                if (value == _regionId) return;
                _regionId = value;
                OnPropertyChanged();
            }
        }

        public string StreetName
        {
            get => _streetName;
            set
            {
                if (value == _streetName) return;
                _streetName = value;
                OnPropertyChanged();
            }
        }

        public string BuildingNumber
        {
            get => _buildingNumber;
            set
            {
                if (value == _buildingNumber) return;
                _buildingNumber = value;
                OnPropertyChanged();
            }
        }

        public string CityName
        {
            get => _cityName;
            set
            {
                if (value == _cityName) return;
                _cityName = value;
                OnPropertyChanged();
            }
        }

        public string CountryName
        {
            get => _countryName;
            set
            {
                if (value == _countryName) return;
                _countryName = value;
                OnPropertyChanged();
            }
        }

        public string RegionName
        {
            get => _regionName;
            set
            {
                if (value == _regionName) return;
                _regionName = value;
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
