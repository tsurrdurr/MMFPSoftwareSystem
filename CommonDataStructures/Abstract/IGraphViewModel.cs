using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MMFPCommonDataStructures
{
    public interface IGraphViewModel
    {
        void PlotGraph(List<Tuple<double, double>> coordinates, string header);
    }
}
