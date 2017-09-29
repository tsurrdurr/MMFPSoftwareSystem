using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMFPSoftwareSystem
{
    public class MainViewModel : INotifyPropertyChanged
    {


        public MainViewModel()
        {

        }


        public Command SampleCommand => _sampleCommand ?? (_sampleCommand = new Command(SampleMethod));


        public string Test
        {
            get => "test";
            set { OnPropertyChanged(nameof(Test)); }
        }


        private Command _sampleCommand;


        private void SampleMethod()
        {
            Debug.Write("method");
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
