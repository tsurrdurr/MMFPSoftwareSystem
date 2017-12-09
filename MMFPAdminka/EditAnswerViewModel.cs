using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MMFPCommonDataStructures;
using MMFPSoftwareSystem;

namespace MMFPAdminka
{
    public class EditAnswerViewModel : Window, INotifyPropertyChanged
    {
        private Question _question;
        private Command _saveCommand;

        public EditAnswerViewModel(Question question)
        {
            Question = question;
        }

        public Question Question
        {
            get => _question;
            set
            {
                if (_question == value) return;
                _question = value;
                OnPropertyChanged(nameof(Question));
            }
        }

        public Command Save => _saveCommand ?? (_saveCommand = new Command(SaveAnswer));

        private void SaveAnswer(Object dialog)
        {
            (dialog as Window).Close();
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