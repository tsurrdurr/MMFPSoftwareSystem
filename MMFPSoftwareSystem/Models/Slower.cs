using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMFPSoftwareSystem
{
    public class Slower : INotifyPropertyChanged
    {
        public Slower(string name, double displacement, double decelerator)
        {
            this.Name = name;
            this.Displacement = displacement;
            this.Decelerator = decelerator;
        }


        public double Displacement
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(Displacement));
                }
            }
        }

        public double Decelerator
        {
            get { return _decelerator; }
            set
            {
                if (_decelerator != value)
                {
                    _decelerator = value;
                    OnPropertyChanged(nameof(Decelerator));
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private string _name;
        private double _value;
        private double _decelerator;

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

