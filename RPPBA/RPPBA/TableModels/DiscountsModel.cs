using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RPPBA.Annotations;

namespace RPPBA.TableModels
{
    public partial class DiscountsModel : INotifyPropertyChanged
    {
        private int _organizationId;
        private int _discount;

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
