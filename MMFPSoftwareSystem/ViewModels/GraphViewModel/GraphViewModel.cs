using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MMFPCommonDataStructures;
using OxyPlot;
using OxyPlot.Series;

namespace MMFPSoftwareSystem
{
    public class GraphViewModel : IGraphViewModel, INotifyPropertyChanged
    {

        public void PlotGraph(List<Tuple<double, double>> coordinates, string header = "Нейтрон")
        {
            var xyeta = Points;
            foreach (var item in TupleToDataPoint(coordinates))
            {
                xyeta.Add(item);
            }
            var series = new LineSeries();
            series.ItemsSource = xyeta;


            MyPlotModel.Series.Add(series);
            MyPlotModel.InvalidatePlot(true);
            //OnPropertyChanged(nameof(Points));
            //Points = xyeta;
            Title = header;
        } 

        public void PlotSeveralGraphs(List<List<Tuple<double, double>>> ListOfCoordinatesLists)
        {
            MyPlotModel = new PlotModel { Title = "Замедление нейтронов" };

            foreach(var list in ListOfCoordinatesLists)
            {
                var series = new LineSeries();
                series.ItemsSource = TupleToDataPoint(list);
                MyPlotModel.Series.Add(series);
            }
            MyPlotModel.InvalidatePlot(true);
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

        public PlotModel MyPlotModel
        {
            get { return _myPlotModel; }
            set
            {
                if(_myPlotModel != value)
                {
                    _myPlotModel = value;
                    OnPropertyChanged(nameof(MyPlotModel));
                }
            }
        }

        public IList<DataPoint> Points
        { 
            get { return _points ?? (_points = new List<DataPoint>()); }
            set
            {
                    _points = value;
                    OnPropertyChanged(nameof(Points));
            }
        }

        private string _title;
        private IList<DataPoint> _points;
        private PlotModel _myPlotModel = new PlotModel();

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
