using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace MMFPCommonDataStructures
{
    public class Question : INotifyPropertyChanged
    {
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

        public IEnumerable<string> Answers
        {
            get { return _answers; }
            set
            {
                if (_answers != value)
                {
                    _answers = value;
                    OnPropertyChanged(nameof(Answers));
                }
            }
        }
        public int? SelectedAnswer
        {
            get { return _selectedAnswer; }
            set
            {
                if (_selectedAnswer != value)
                {
                    _selectedAnswer = value;
                    OnPropertyChanged(nameof(SelectedAnswer));
                }
            }
        }

        public override string ToString()
        {
            return Text;
        }

        private string _text;
        private IEnumerable<string> _answers;
        private int? _selectedAnswer;

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