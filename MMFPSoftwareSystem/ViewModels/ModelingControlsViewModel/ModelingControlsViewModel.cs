using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMFPCommonDataStructures;
using Newtonsoft.Json;

namespace MMFPSoftwareSystem
{
    public class ModelingControlsViewModel : IModelingControlsViewModel, INotifyPropertyChanged
    {
        public ModelingControlsViewModel(IGraphViewModel graph)
        {
            this.Graph = graph;
        }

        [JsonIgnore]
        public Command PlotLogarithmCommand => _plotLogarithmCommand ?? (_plotLogarithmCommand = new Command(PlotLogarithm));

        public string LogarithmUpperLimitString
        {
            get { return _logarithmUpperLimitString.ToString(); }
            set
            {
                if (Double.TryParse(value, out var doubleValue))
                {
                    if (_logarithmUpperLimitString != doubleValue)
                    {
                        _logarithmUpperLimitString = doubleValue;
                        OnPropertyChanged(nameof(LogarithmUpperLimitString));
                    }
                }
            }
        }

        private IGraphViewModel Graph;
        private Command _plotLogarithmCommand;
        private double _logarithmUpperLimitString;

        private void PlotLogarithm()
        {
            var title = "ln(x)";
            var points = new List<Tuple<double, double>>();
            for (int i = 0; i < _logarithmUpperLimitString; i++)
            {
                points.Add(Tuplify(i, Math.Log(i)));
            }
            if (points.Count > 1)
            {
                Graph.PlotGraph(points, title);
            }
        }

        private Tuple<double, double> Tuplify(double x, double y)
        {
            return new Tuple<double, double>(x, y);
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
