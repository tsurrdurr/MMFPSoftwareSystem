using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;

namespace MMFPCommonDataStructures
{
    public class Answer : INotifyPropertyChanged
    {

        private string _text;
        private bool _isCorrect = false;

        public string Text
        {
            get { return _text; }
            set
            { 
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged(nameof(Text));
                }
            }
        }

        public bool IsCorrect
        {
            get => _isCorrect;
            set
            {
                if (_isCorrect == value) return;
                _isCorrect = value;
                OnPropertyChanged(nameof(IsCorrect));
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