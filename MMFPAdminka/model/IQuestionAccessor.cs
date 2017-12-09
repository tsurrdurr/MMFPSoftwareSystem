using System.Collections.Generic;
using MMFPCommonDataStructures;

namespace MMFPAdminka.model
{
    public interface IQuestionAccessor
    {
        List<QuestionSection> LoadQuestionSections();
        void SaveQuestionSections(List<QuestionSection> sections);
    }
}