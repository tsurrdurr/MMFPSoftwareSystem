using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using MMFPAdminka.model;
using MMFPCommonDataStructures;
using MMFPSoftwareSystem;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MMFPAdminka
{
    class MainViewModel : Window, INotifyPropertyChanged
    {
        private IQuestionAccessor _questionAccessor;
        private QuestionSet _questions;
        private ObservableCollection<QuestionSection> _questionSetSections;
        private Command _addSectionCommand;
        private Command _addQuestionCommand;
        private Command _addAnswerCommand;
        private QuestionSection _selectedSection;
        private ObservableCollection<Question> _sectionsQuestions;
        private Question _selectedQuestion;
        private Command _questionSectionCRUDCommand;
        private Command _questionCRUDCommand;
        private Command _answerCrudCommand;
        private ObservableCollection<string> _answers;
        private string _selectedAnswer;
        private Command _openTestCommand;
        private Command _saveTestCommand;

        public QuestionSet Questions
        {
            get => _questions ?? new QuestionSet();
            set
            {
                if (_questions == value) return;
                _questions = value;
                OnPropertyChanged(nameof(Questions));
            }
        }

        public ObservableCollection<QuestionSection> QuestionSetSections
        {
            get => Questions.Sections ?? new ObservableCollection<QuestionSection>();
            set
            {
                if (Questions.Sections == value) return;
                Questions.Sections = value;
                OnPropertyChanged(nameof(QuestionSetSections));
            }
        }


        public ObservableCollection<Question> SectionsQuestions
        {
            get { return _sectionsQuestions; }
            set
            {
                if (_sectionsQuestions == value) return;
                _sectionsQuestions = value;
                OnPropertyChanged(nameof(SectionsQuestions));
            }
        }

        public ObservableCollection<string> Answers
        {
            get { return _answers; }
            set
            {
                if (_answers == value) return;
                _answers = value;
                OnPropertyChanged(nameof(Answers));
            }
        }

        public QuestionSection SelectedSection
        {
            get => _selectedSection;
            set
            {
                if (_selectedSection == value) return;
                if (value != null)
                {
                    SectionsQuestions = value.Questions as ObservableCollection<Question>;
                    SelectedQuestion = SectionsQuestions.FirstOrDefault();
                    _selectedSection = value;
                    OnPropertyChanged(nameof(SelectedSection));
                    OnPropertyChanged(nameof(QuestionSetSections));
                }

            }
        }

        

        public Question SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                if (_selectedQuestion == value || value == null) return;
                _selectedQuestion = value;
                Answers = _selectedQuestion.Answers as ObservableCollection<string>;
                OnPropertyChanged(nameof(SelectedQuestion));
            }
        }

        public string SelectedAnswer
        {
            get { return _selectedAnswer; }
            set
            {
                if (_selectedAnswer == value) return;
                _selectedAnswer = value;
                OnPropertyChanged(nameof(SelectedAnswer));
            }
        }
        
        public Command OpenTestCommand => _openTestCommand ?? (_openTestCommand = new Command(OpenTest));
        public Command SaveTestCommand => _saveTestCommand ?? (_saveTestCommand = new Command(SaveTest));
        public Command AddSectionCommand => _addSectionCommand ?? (_addSectionCommand = new Command(AddSection));
        public Command AddQuestionCommand => _addQuestionCommand ?? (_addQuestionCommand = new Command(AddQuestion));
        public Command AddAnswerCommand => _addAnswerCommand ?? (_addAnswerCommand = new Command(AddAnswer));

        public Command QuestionSectionCRUDCommand => _questionSectionCRUDCommand ?? (_questionSectionCRUDCommand = new Command(EditSection));

        public Command QuestionCRUDCommand => _questionCRUDCommand ?? (_questionCRUDCommand = new Command(EditQuestion));
        public Command AnswerCRUDCommand => _answerCrudCommand ?? (_answerCrudCommand = new Command(EditAnswer));



        private void OpenTest()
        {
            try
            {
                var dialog = new OpenFileDialog
                {
                    Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"
                };
                if (dialog.ShowDialog() == true)
                {
                    var json = File.ReadAllText(dialog.FileName);
                    ParseTest(json);
                    OnPropertyChanged(nameof(Questions));
                    OnPropertyChanged(nameof(QuestionSetSections));
                    OnPropertyChanged(nameof(SectionsQuestions));

                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        private void ParseTest(string json)
        {
            Questions = JsonConvert.DeserializeObject<QuestionSet>(json);
        }

        private void SaveTest()
        {
            var jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            string output = JsonConvert.SerializeObject(Questions, jsonSettings);
            var dialog = new SaveFileDialog
            {
                FileName = "Новый тест",
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",

            };
            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, output);
            }
        }

        private void EditSection()
        {
            var editQuestionSectionDialog = new EditQuestionSectionDialog(SelectedSection);
            editQuestionSectionDialog.Show();
        }

        private void EditQuestion()
        {
            var editQuestionDialog = new EditQuestionDialog(SelectedQuestion);
            editQuestionDialog.Show();
        }

        private void EditAnswer()
        {
            //var editAnswerDialog = new EditAnswerDialog(SelectedAnswer);
            //editAnswerDialog.Show();
        }

        //        
        //        public ICommand AddQuestionCommand
        //        {
        //            get
        //            {
        //                return _addQuestionCommand ?? (_addQuestionCommand = new Command((questionSection) =>
        //                {
        //                    (questionSection as QuestionSection)?.Questions?.ToList().Add(new Question
        //                    {
        //                        Answers = new ObservableCollection<string>(),
        //                        SelectedAnswer = 0,
        //                        Text = "Новый вопрос"
        //                    });
        //                }));
        //            }
        //        }

        private void AddQuestion()
        {
            SectionsQuestions.Add(new Question
            {
                Answers = new ObservableCollection<string>(),
                SelectedAnswer = 0,
                Text = "Новый вопрос"
            });
        }

        private void AddAnswer()
        {
            Answers.Add("Новый ответ");
        }

        public MainViewModel()
        {
            _questionAccessor = QuestionAccesors.GetAccessor(QuestionAccessorType.STUB);
            Questions = _questionAccessor.LoadQuestionSet();
        }

        private void AddSection()
        {
            _questionSetSections.Add(new QuestionSection
            {
                Name = "Новый раздел",
                Questions = new ObservableCollection<Question>()
            });
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
//            OnPropertyChanged(new PropertyChangedEventArgs("DisplayMember"));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);

        }
        #endregion
    }
}
