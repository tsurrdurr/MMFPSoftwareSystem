using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MMFPCommonDataStructures;
using OxyPlot;

namespace MMFPSoftwareSystem
{
    public class GraphViewModel : IGraphViewModel, INotifyPropertyChanged
    {

        public void PlotGraph(List<Tuple<double, double>> coordinates, string header = "График")
        {
            var xyeta = Points;
            foreach (var item in TupleToDataPoint(coordinates))
            {
                xyeta.Add(item);
            }
            //OnPropertyChanged(nameof(Points));
            Points = xyeta;
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
        { //return _currentTopic ?? (_currentTopic = new Topic());
            get { return _points ?? (_points = new List<DataPoint>()); }
            set
            {

                    _points = value;
                    OnPropertyChanged(nameof(Points));
                
            }
        }

        private string _title;
        private IList<DataPoint> _points;

        private static List<DataPoint> TupleToDataPoint(List<Tuple<double, double>> coordinates)
        {
            var list = new List<DataPoint>();
            coordinates.ForEach(x => list.Add(new DataPoint(x.Item1, x.Item2)));
            return list;
        }
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
