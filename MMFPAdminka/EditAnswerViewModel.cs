using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private int _selectedAnswerIndex;
        private Command _saveCommand;

        public EditAnswerViewModel(Question question, int selectedAnswerIndex)
        {
            Question = question;
            _selectedAnswerIndex = selectedAnswerIndex;
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

        public Answer Answer
        {
            get => Question.Answers.ToList()[_selectedAnswerIndex];
            set => ((ObservableCollection<Answer>) Question.Answers)[_selectedAnswerIndex] = value;
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