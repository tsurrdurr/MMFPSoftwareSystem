using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMFPSoftwareSystem
{
    public class SlowersList : INotifyPropertyChanged
    {
        private ObservableCollection<Slower> _slowers = GenerateDefault();

        private static ObservableCollection<Slower> GenerateDefault()
        {
            return new ObservableCollection<Slower>
            {
                new Slower("Вода", 6 * 27, 18),
                new Slower("Тяжёлая вода", 6 * 120, 20),
                new Slower("Бериллий", 6 * 98, 4),
                new Slower("Оксид бериллия", 6 * 105, 20),
                new Slower("Графит", 6 * 350, 14)
            };
        }

        public ObservableCollection<Slower> Slowers
        {
            get { return _slowers; }
            set
            {
                if (_slowers != value)
                {
                    _slowers = value;
                    OnPropertyChanged(nameof(Slowers));
                }
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);

        }
        #endregion
    }
}

