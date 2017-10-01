using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMFPSoftwareSystem.Models
{
    public class Topic
    {
        public Topic(string Name,
                     ITheoryViewModel theory,
                     IModelingControlsViewModel modelingControls,
                     IGraphViewModel graph,
                     ITestingViewModel testing)
        {
            this.Name = Name;
            this.theory = theory;
            this.graph = graph;
            this.modelingControls = modelingControls;
            this.testing = testing;
        }

        public Topic()
        {
            string Name = "empty";
        }

        private string Name;
        public ITheoryViewModel theory;
        public IModelingControlsViewModel modelingControls;
        public IGraphViewModel graph;
        public ITestingViewModel testing;

        public override string ToString()
        {
            return Name;
        }
    }
}
