using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;

namespace MMFPCommonDataStructures
{
    public class Question : INotifyPropertyChanged
    {

        public byte[] cryptedAnswer;

        private string _text;
        private IEnumerable<Answer> _answers;
        private int? _selectedAnswer;
        private byte[] _salt;

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

        public IEnumerable<Answer> Answers
        {
            get
            {
                if(_answers != null) return _answers;
                var tempshnyaga = new ObservableCollection<Answer>();
                tempshnyaga.CollectionChanged += (s, e) =>
                {
                    if (e.NewItems?.Count > 0 && (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Replace))
                    {

                        foreach (var ans in e.NewItems.OfType<Answer>())
                        {
                            if (_answers.Count() <= 1) ans.IsCorrect = true;
                            ans.PropertyChanged += UpdateCorrectAnswer;
                        }
                    }
                    if (e.OldItems?.Count > 0 && (e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Replace || e.Action == NotifyCollectionChangedAction.Reset))
                    {
                        foreach (var ans in e.OldItems.OfType<Answer>())
                        {
                            ans.PropertyChanged -= UpdateCorrectAnswer;
                        }
                    }
                };
                return _answers = tempshnyaga;
            } 
            set
            {
                
            }
        }

        private void UpdateCorrectAnswer(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Answer.IsCorrect))
            {
                Answer ans = sender as Answer;
                ans.PropertyChanged -= UpdateCorrectAnswer;
                var correctIndex = Answers.ToList().IndexOf(ans);
                foreach (var item in Answers)
                {
                    if (item != ans)
                    {
                        item.PropertyChanged -= UpdateCorrectAnswer;
                        item.IsCorrect = false;
                        item.PropertyChanged += UpdateCorrectAnswer;

                    }
                }
                ans.IsCorrect = true;
                SelectedAnswer = correctIndex;
                ans.PropertyChanged += UpdateCorrectAnswer;

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

        public byte[] Salt
        {
            get { return _salt; }
            set
            {
                if (_salt != value)
                {
                    _salt = value;
                    OnPropertyChanged(nameof(Salt));
                }
            }
        }

        public void EncodeAnswer(int? selected)
        {
            if (selected == null) return;
            var cr = new Crypto.Crypto();
            Salt = cr.CreateSalt();
            cryptedAnswer = cr.Encode(2, Salt);
        }

        public int? DecodeAnswer()
        {
            if (Salt == null || cryptedAnswer == null) return null;
            var cr = new Crypto.Crypto();
            var dec = cr.Decode(cryptedAnswer, Salt);
            return dec;
        }

        public override string ToString()
        {
            return Text;
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