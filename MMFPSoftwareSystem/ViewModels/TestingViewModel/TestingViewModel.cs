using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
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

        private void SaveResult()
        {
            questionSet.TestingFinished = DateTime.Now;
            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            string output = JsonConvert.SerializeObject(questionSet, jsonSettings);
            var dialog = new SaveFileDialog
            {
                FileName = String.Format("{0} {1}", questionSet.Name, "studentname"),
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",

            };
            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, output);
                questionSet = null;
                IsCurrentlyTesting = false;
            }
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

        public bool IsCurrentlyTesting
        {
            get { return _isCurrentlyTesting; }
            set
            {
                if (_isCurrentlyTesting != value)
                {
                    _isCurrentlyTesting = value;
                    OnPropertyChanged(nameof(IsCurrentlyTesting));
                }
            }
        }

        private void StartTest()
        {
            IsCurrentlyTesting = true;
            questionSet = SelectedQuestionSet;
            questionSet.TestingStarted = DateTime.Now;
        }

        private void FinishTest()
        {
            SaveResult();
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
        private bool _isCurrentlyTesting = false;
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
