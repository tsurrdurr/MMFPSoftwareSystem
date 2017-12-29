using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMFPCommonDataStructures;

namespace MMFPSoftwareSystem.Helpers
{
    class TestChecker
    {
        /*
         *  Выполняет проверку сета вопросов. Возвращает процент правильных ответов.
         */
        public static int checkTest(QuestionSet questionSet)
        {
            int correctAnswerCount = 0;
            int totalQuestionsCount = 0;
            foreach (var questionSection in questionSet.Sections)
            {
                foreach (var question in questionSection.Questions)
                {
                    totalQuestionsCount++;
                    if (question.IsCorrect())
                    {
                        correctAnswerCount++;
                        (question.Answers as ObservableCollection<Answer>)[question.SelectedAnswer ?? 0].IsCorrect = true;
                    }
                }
            }
            int result = (int)((float)correctAnswerCount / totalQuestionsCount * 100);
            return result;
        }
    }
}
