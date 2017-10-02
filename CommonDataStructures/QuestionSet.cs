using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMFPCommonDataStructures
{
    public class QuestionSet
    {
        public IEnumerable<QuestionSection> Sections;
        public string Name;
        public DateTime SetCreatedDateTime;
        public DateTime SetModifiedDateTime;
        public DateTime? TestingStarted = null;
        public DateTime? TestingFinished = null;
        public string StudentName = null;
        public string StudentGroup = null;
    }
}
