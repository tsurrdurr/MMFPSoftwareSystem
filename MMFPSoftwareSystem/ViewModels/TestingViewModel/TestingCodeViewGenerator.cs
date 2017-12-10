using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using MMFPCommonDataStructures;

namespace MMFPSoftwareSystem.Views
{
    class TestingCodeViewGenerator : UserControl
    {

        public TestingCodeViewGenerator()
        {

        }

        public static readonly DependencyProperty QuestionsProperty = DependencyProperty.Register("Questions", typeof(QuestionSet), typeof(TestingCodeViewGenerator));

        public QuestionSet Questions
        {
            get { return (QuestionSet)GetValue(QuestionsProperty); }
            set { SetValue(QuestionsProperty, value); }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == QuestionsProperty)
            {
                if (Questions != null)
                {
                    LoadQuestions(Questions);
                }
                else
                {
                    Content = null;
                }
            }
        }

        private void LoadQuestions(QuestionSet questions)
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            var panel = new StackPanel();
            IAddChild container = panel;
            if (Questions.Sections == null) { this.UpdateLayout(); return;};
            var setText = new TextBlock
            {
                Text = "Тема теста: \"" + Questions.Name + "\""
            };
            int sectionNumber = 1;
            container.AddChild(setText);
            container.AddChild(new Separator());
            foreach (var section in Questions.Sections)
            {
                int questionNumber = 1;
                var sectionText = new TextBlock
                {
                    Text = sectionNumber + ") Раздел \"" + section.Name + "\""
                };
                container.AddChild(sectionText);
                foreach (var question in section.Questions)
                {
                    var questionText = new TextBlock
                    {
                        Text = questionNumber + ". " + question.Text
                    };
                    container.AddChild(questionText);
                    int answerNumber = 1;
                    foreach (var answer in question.Answers)
                    {
                        var radio = new RadioButton
                        {
                            Content = answer,
                            GroupName = "Group_" + sectionNumber + "_" + questionNumber,
                            Tag = new Tuple<Question, Answer>(question, answer)
                        };
                        radio.Checked += Radio_Checked;
                        container.AddChild(radio);
                        answerNumber++;
                    }
                    questionNumber++;
                }
                container.AddChild(new Separator());
                sectionNumber++;
            }

            container = this;
            container.AddChild(panel);
        }

        private void Radio_Checked(object sender, RoutedEventArgs e)
        {
            var tag = (sender as RadioButton).Tag as Tuple<Question, Answer>;
            tag.Item1.SelectedAnswer = tag.Item1.Answers.ToList().IndexOf(tag.Item2);
        }

    }
}
