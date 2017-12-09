using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;

namespace MMFPCommonDataStructures
{
    public class Question : INotifyPropertyChanged
    {

        public byte[] cryptedAnswer;

        private string _text;
        private IEnumerable<string> _answers;
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