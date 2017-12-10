using System.Collections.Generic;
using System.Collections.ObjectModel;
using MMFPCommonDataStructures;

namespace MMFPAdminka.model
{
    public class StubQuestionAccessor : IQuestionAccessor
    {
        public QuestionSet LoadQuestionSet()
        {
            var content = new ObservableCollection<QuestionSection>
            {
                new QuestionSection
                {
                    Name = "Исследование процессов замедления",
                    Questions = new ObservableCollection<Question>(new List<Question>
                    {
                        new Question
                        {
                            Answers = new ObservableCollection<Answer>(new List<Answer>
                            {
                                new Answer { Text = "Никак" },
                                new Answer { Text = "Выпивают водку" },
                                new Answer { Text = "Концентрируются на продаже природных ресурсов" },
                            }),
                            SelectedAnswer = 2,
                            Text = "Как нейтроны замедляются в реакторе?"
                        },
                        new Question
                        {
                            Answers = new ObservableCollection<Answer>(new List<Answer>
                            {
                                new Answer { Text = "у Васи" },
                                new Answer { Text = "У Пети" },
                                new Answer { Text = "у нейтрона" },
                                new Answer { Text = "промолчать" }
                            }),
                            SelectedAnswer = 1,
                            Text = "Студент Вася нажал кнопку \"Ускорить\" вместо \"Замедлить\", а студент Петя прогулял все лекции Рыбиной. У какого объекта скорость вылета больше?"
                        }
                    })
                }
            };
            return new QuestionSet {Sections = content};
        }

        public void SaveQuestionSections(List<QuestionSection> sections)
        {
            throw new System.NotImplementedException();
        }
    }
}