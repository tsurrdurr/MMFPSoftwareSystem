using System.Collections;
using System.Collections.Generic;

namespace MMFPCommonDataStructures
{
    public class Question
    {
        public string Text;
        public IEnumerable<string> Answers;
        public int? SelectedAnswer;
    }
}