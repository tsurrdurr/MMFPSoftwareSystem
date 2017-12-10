using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMFPCommonDataStructures;
using Newtonsoft.Json;

namespace MMFPSoftwareSystem
{
    public class TestingViewModel : ITestingViewModel, INotifyPropertyChanged
    {
        public TestingViewModel(MainViewModel mainVM)
        {
            this.mainVM = mainVM;
            AvailableTests = ReadAvailableTestsFiles();

        }

        private List<QuestionSet> ReadAvailableTestsFiles()
        {
            string[] files = Directory.GetFiles(@"Resources\Tests", "*.json", SearchOption.AllDirectories);
            var results = new List<QuestionSet>();
            foreach (var file in files)
            {
                var json = File.ReadAllText(file);
                var item = JsonConvert.DeserializeObject<QuestionSet>(json);
                if (item != null) results.Add(item);
            }
            return results;
        }

        private MainViewModel mainVM;
        public Command StartTestCommand => _startTestCommand ?? (_startTestCommand = new Command(StartTest));
        public Command FinishTestCommand => _finishTestCommand ?? (_finishTestCommand = new Command(FinishTest));

        private void FinishTest()
        {
            IsNotCurrentlyTesting = false;
            SaveResult();
        }

        private void SaveResult()
        {
            //TODO: add code
        }

        public List<QuestionSet> AvailableTests
        {
            get { return _availableTests; }
            set
            {
                if (_availableTests != value)
                {
                    _availableTests = value;
                    OnPropertyChanged(nameof(AvailableTests));
                }
            }
        }

        public bool IsNotCurrentlyTesting
        {
            get { return _isNotCurrentlyTesting; }
            set
            {
                if (_isNotCurrentlyTesting != value)
                {
                    _isNotCurrentlyTesting = value;
                    OnPropertyChanged(nameof(IsNotCurrentlyTesting));
                }
            }
        }

        private void StartTest()
        {
            IsNotCurrentlyTesting = true;
            questionSet = SelectedQuestionSet;
            //GetQuestions(mainVM.CreatePredefinedQuestionSet());
        }

        public QuestionSet SelectedQuestionSet
        {
            get { return _selectedQuestionSet; }
            set
            {
                if (_selectedQuestionSet != value)
                {
                    _selectedQuestionSet = value;
                    OnPropertyChanged(nameof(SelectedQuestionSet));
                }
            }
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
        private List<QuestionSet> _availableTests;
        private bool _isNotCurrentlyTesting = true;
        private Command _finishTestCommand;
        private QuestionSet _selectedQuestionSet;

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
