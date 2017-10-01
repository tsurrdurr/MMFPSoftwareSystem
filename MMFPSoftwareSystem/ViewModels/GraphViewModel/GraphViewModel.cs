using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MMFPSoftwareSystem.Models;
using OxyPlot;

namespace MMFPSoftwareSystem
{
    public class GraphViewModel : IGraphViewModel, INotifyPropertyChanged
    {
        public GraphViewModel()
        {
            
        }

        public void PlotGraph(List<DataPoint> coordinates, string header = "График")
        {
            Points = coordinates;
            Title = header;
        }

        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public IList<DataPoint> Points
        {
            get { return _points; }
            set
            {
                if (_points != value)
                {
                    _points = value;
                    OnPropertyChanged(nameof(Points));
                }
            }
        }

        private string _title;
        private IList<DataPoint> _points;

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);

        }
        #endregion
    }
}
