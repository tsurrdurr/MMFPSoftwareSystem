using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMFPCommonDataStructures
{
    public interface ITestingViewModel
    {
        QuestionSet questionSet { get; set; }
        void GetQuestions(QuestionSet set);
        void GenerateResult();
        void GeneratePDF();
    }
}
