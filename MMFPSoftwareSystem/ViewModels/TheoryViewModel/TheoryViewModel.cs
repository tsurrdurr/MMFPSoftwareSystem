using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMFPSoftwareSystem
{
    public class TheoryViewModel : Models.ITheoryViewModel, INotifyPropertyChanged
    {

        public string HtmlPath
        {
            get { return _htmlPath; }
            set
            {
                if(_htmlPath != value)
                {
                    _htmlPath = value;
                    OnPropertyChanged(nameof(HtmlPath));
                }
            }
        }

        public void OpenDocument(string path)
        {
            HtmlPath = path;
        }

        private string _htmlPath = null;
        
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
