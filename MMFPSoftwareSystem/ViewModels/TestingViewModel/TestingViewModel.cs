using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMFPCommonDataStructures;

namespace MMFPSoftwareSystem
{
    public class TestingViewModel : ITestingViewModel, INotifyPropertyChanged
    {
        public TestingViewModel(MainViewModel mainVM)
        {
            this.mainVM = mainVM;
        }

        private MainViewModel mainVM;
        public Command StartTestCommand => _startTestCommand ?? (_startTestCommand = new Command(StartTest));

        private void StartTest()
        {
            GetQuestions(mainVM.CreatePredefinedQuestionSet());
        }

        public QuestionSet questionSet
        {
            get { return _questionSet; }
            set
            {
                if (_questionSet != value)
                {
                    _questionSet = value;
                    OnPropertyChanged(nameof(questionSet));
                }
            }
        }

        public void GetQuestions(QuestionSet set)
        {
            questionSet = set;
        }

        public void GenerateResult()
        {
            throw new NotImplementedException();
        }

        public void GeneratePDF()
        {
            throw new NotImplementedException();
        }

        private QuestionSet _questionSet = null;
        private Command _startTestCommand;

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
