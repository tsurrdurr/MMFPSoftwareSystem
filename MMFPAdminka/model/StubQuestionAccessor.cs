using System.Collections.Generic;
using System.Collections.ObjectModel;
using MMFPCommonDataStructures;

namespace MMFPAdminka.model
{
    public class StubQuestionAccessor : IQuestionAccessor
    {
        public List<QuestionSection> LoadQuestionSections()
        {
            return new List<QuestionSection>
            {
                new QuestionSection
                {
                    Name = "Исследование процессов замедления",
                    Questions = new ObservableCollection<Question>(new List<Question>
                    {
                        new Question
                        {
                            Answers = new ObservableCollection<string>(new List<string>()
                            {
                                "Никак",
                                "Выпивают водку",
                                "Концентрируются на продаже природных ресурсов"
                            }),
                            SelectedAnswer = 2,
                            Text = "Как нейтроны замедляются в реакторе?"
                        },
                        new Question
                        {
                            Answers = new ObservableCollection<string>(new List<string>
                            {
                                "у Васи", "У Пети", "у нейтрона", "промолчать"
                            }),
                            SelectedAnswer = 1,
                            Text = "Студент Вася нажал кнопку \"Ускорить\" вместо \"Замедлить\", а студент Петя прогулял все лекции Рыбиной. У какого объекта скорость вылета больше?"
                        }
                    })
                }
            };
        }

        public void SaveQuestionSections(List<QuestionSection> sections)
        {
            throw new System.NotImplementedException();
        }
    }
}