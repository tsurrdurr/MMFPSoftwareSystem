using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMFPCommonDataStructures;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

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
            get => _logarithmUpperLimitString.ToString();
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
        private ObservableCollection<Slower> _slowersType;
        private double _startingEnergy = 12d;
        private double _finishingEnergy = 3d;
        private int _neutronsAmount = 3;
        private Slower _selectedSlower;

        private void PlotLogarithm()
        {
            var title = "ln(x)";
            var points = new List<Tuple<double, double>>();
            var points2 = new List<Tuple<double, double>>();
            for (int i = 0; i < _logarithmUpperLimitString; i++)
            {
                points.Add(Tuplify(i, Math.Log(i)));
                points2.Add(Tuplify(i, Math.Log(i) + 3));
            }
            if (points.Count > 1)
            {
                //Graph.PlotGraph(points, title);
            }
            //Graph.PlotGraph(points2, null);
            Graph.PlotSeveralGraphs(new List<List<Tuple<double, double>>> { points, points2 });
            
        }

        public ObservableCollection<Slower> SlowersType
        {
            get { return _slowersType ?? (_slowersType = new SlowersList().Slowers); }
            set
            {
                if(_slowersType != value)
                {
                    _slowersType = value;
                    OnPropertyChanged(nameof(SlowersType));
                }
            }
        }

        public Slower SelectedSlower
        {
            get { return _selectedSlower ?? (_selectedSlower = SlowersType.FirstOrDefault()); }
            set
            {
                if (_selectedSlower != value)
                {
                    _selectedSlower = value;
                    OnPropertyChanged(nameof(SelectedSlower));
                }
            }
        }

        public double StartingEnegry
        {
            get { return _startingEnergy; }
            set
            {
                if(_startingEnergy != value)
                {
                    _startingEnergy = value;
                    OnPropertyChanged(nameof(StartingEnegry));
                }
            }
        }

        public double FinishingEnergy
        {
            get { return _finishingEnergy; }
            set
            {
                if (_finishingEnergy != value)
                {
                    _finishingEnergy = value;
                    OnPropertyChanged(nameof(FinishingEnergy));
                }
            }
        }
        public int NeutronsAmount
        {
            get { return _neutronsAmount; }
            set
            {
                if (_neutronsAmount != value)
                {
                    _neutronsAmount = value;
                    OnPropertyChanged(nameof(NeutronsAmount));
                }
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
