using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MMFPCommonDataStructures
{
    public class QuestionSet : INotifyPropertyChanged
    {
        public IEnumerable<QuestionSection> Sections
        {
            get { return _sections; }
            set
            {
                if (_sections != value)
                {
                    _sections = value;
                    OnPropertyChanged(nameof(Sections));
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

        public DateTime CreatedDateTime;
        public DateTime ModifiedDateTime;
        public DateTime? TestingStarted = null;
        public DateTime? TestingFinished = null;
        public string StudentName = null;
        public string StudentGroup = null;

        private string _name;
        private IEnumerable<QuestionSection> _sections;

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
