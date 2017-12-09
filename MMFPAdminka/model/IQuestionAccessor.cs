using System.Collections.Generic;
using MMFPCommonDataStructures;

namespace MMFPAdminka.model
{
    public interface IQuestionAccessor
    {
        QuestionSet LoadQuestionSet();
        void SaveQuestionSections(List<QuestionSection> sections);
    }
}