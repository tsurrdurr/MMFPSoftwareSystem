using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OxyPlot;

namespace MMFPSoftwareSystem.Models
{
    public interface IGraphViewModel
    {
        void PlotGraph(List<DataPoint> coordinates, string header);
    }
}
