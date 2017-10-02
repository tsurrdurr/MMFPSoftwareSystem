using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMFPCommonDataStructures;

namespace MMFPAdminka
{
    class MainViewModel : INotifyPropertyChanged
    {
        private QuestionSet _questions;

        public QuestionSet Questions
        {
            get { return _questions; }
            set
            {
                if (_questions != value)
                {
                    _questions = value;
                    OnPropertyChanged(nameof(Questions));
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
