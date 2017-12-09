using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace MMFPCommonDataStructures
{
    public class QuestionSection : INotifyPropertyChanged
    {
        private string _name;
        private IEnumerable<Question> _questions;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                    OnPropertyChanged(nameof(QuestionSection));
                }
            }
        }

        public IEnumerable<Question> Questions
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

        public override string ToString()
        {
            return Name;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            OnPropertyChanged(new PropertyChangedEventArgs("DisplayMember"));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);

        }
        #endregion
    }
}