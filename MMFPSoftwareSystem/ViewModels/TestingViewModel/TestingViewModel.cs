using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using MMFPCommonDataStructures;
using MMFPSoftwareSystem.Views.Windows;
using Newtonsoft.Json;

namespace MMFPSoftwareSystem
{
    public class TestingViewModel : ITestingViewModel, INotifyPropertyChanged
    {
        public TestingViewModel(MainViewModel mainVM)
        {
            this.mainVM = mainVM;
            AvailableTests = ReadAvailableTestsFiles();
            groups = ReadAvailableGroups();
        }

        private GroupSet dummySet = new GroupSet { Groups = new ObservableCollection<Group> { new Group { Name = "Неизвестная группа", Students = new ObservableCollection<Student> { new Student { Name = "Неизвестный студент" } } } } };

        private GroupSet ReadAvailableGroups()
        {
            var results = new GroupSet();
            try
            {
                string[] files = Directory.GetFiles(@"Resources\Students", "*.json", SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    var json = File.ReadAllText(file);
                    var item = JsonConvert.DeserializeObject<GroupSet>(json);
                    if (item != null)
                    {
                        foreach (var gr in item.Groups)
                        {
                            results.Groups.Add(gr);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            return results.Groups.Count > 0 ? results : dummySet;
        }

        private GroupSet groups;

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
        private string _testResult;

        public String TestResult
        {
            get => _testResult;
            set
            {
                if (_testResult == value) return;
                _testResult = value;
                OnPropertyChanged(nameof(TestResult));
            }
        }
        public Command StartTestCommand => _startTestCommand ?? (_startTestCommand = new Command(StartTest));
        public Command FinishTestCommand => _finishTestCommand ?? (_finishTestCommand = new Command(FinishTest));
        public Command CheckTestCommand => _checkTestCommand ?? (_checkTestCommand = new Command(CheckTest));


        private void SaveResult()
        {
            questionSet.TestingFinished = DateTime.Now;
            questionSet.StudentName = studentName;
            questionSet.StudentGroup = groupName;
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
                    SelectedQuestionSet = _availableTests.FirstOrDefault();
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

        public bool TestIsCheckable
        {
            get { return _testIsCheckable; }
            set
            {
                if (_testIsCheckable != value)
                {
                    _testIsCheckable = value;
                    OnPropertyChanged(nameof(TestIsCheckable));
                }
            }
        }


        public bool TestIsFinished
        {
            get { return _testIsFinished; }
            set
            {
                if (_testIsFinished != value)
                {
                    _testIsFinished = value;
                    OnPropertyChanged(nameof(TestIsFinished));
                }
            }
        }

        private void StartTest()
        {
            var datacontext = new StudentSelectionViewModel(groups);
            var window = new StudentSelectionWindow
            {
                DataContext = datacontext
            };
            if ((bool)window.ShowDialog())
            {
                studentName = datacontext.SelectedStudent.Name;
                groupName = datacontext.SelectedGroup.Name;
                IsCurrentlyTesting = true;
                questionSet = SelectedQuestionSet;
                questionSet.TestingStarted = DateTime.Now;
            }
        }

        private string studentName;
        private string groupName;

        private void FinishTest()
        {
            SaveResult();
            if(!TestIsFinished) CheckTest();
            TestIsFinished = true;
        }

        private void CheckTest()
        {
            //int correctPercent = TestChecker.checkTest(SelectedQuestionSet);
            //TestResult = $"{correctPercent} % правильных ответов.";
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
        private bool _testIsCheckable = true;
        private Command _checkTestCommand;
        private bool _testIsFinished;

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
