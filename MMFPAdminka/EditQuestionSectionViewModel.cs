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
    public class EditQuestionSectionViewModel : Window, INotifyPropertyChanged
    {
        private QuestionSection _questionSection;
        private Command _saveCommand;

        public EditQuestionSectionViewModel(QuestionSection questionSection)
        {
            QuestionSection = questionSection;
        }

        public QuestionSection QuestionSection
        {
            get => _questionSection;
            set
            {
                if (_questionSection == value) return;
                _questionSection = value;
                OnPropertyChanged(nameof(QuestionSection));

            }
        }

        public Command Save => _saveCommand ?? (_saveCommand = new Command(SaveQuestionSectionName));

        private void SaveQuestionSectionName(Object dialog)
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