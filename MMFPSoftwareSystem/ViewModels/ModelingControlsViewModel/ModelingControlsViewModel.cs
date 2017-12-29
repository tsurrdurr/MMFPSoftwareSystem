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
            var graphs = new List<List<Tuple<double, double>>>();
            for(int i = 0; i < NeutronsAmount; i++)
            {
                graphs.Add(GenerateTraectory(SelectedSlower, FinishingEnergy, StartingEnegry, NeutronsAmount));
            };
            //var title = "ln(x)";
            //var points = new List<Tuple<double, double>>();
            //var points2 = new List<Tuple<double, double>>();
            //for (int i = 0; i < _logarithmUpperLimitString; i++)
            //{
            //    points.Add(Tuplify(i, Math.Log(i)));
            //    points2.Add(Tuplify(i, Math.Log(i) + 3));
            //}
            //if (points.Count > 1)
            //{
            //    //Graph.PlotGraph(points, title);
            //}
            ////Graph.PlotGraph(points2, null);
            Graph.PlotSeveralGraphs(graphs);
            
        }
        static float NextFloat(Random random)
        {
            double mantissa = (random.NextDouble() * 2.0) - 1.0;
            double exponent = Math.Pow(2.0, random.Next(0, 1));
            return (float)(mantissa * exponent);
        }

        private double getRand()
        {
            return 2 * NextFloat(rand) - 1;
        }

        private List<Tuple<double, double>> GenerateTraectory(Slower selectedSlower, double finishing, double starting, int amount)
        {
            var displ = selectedSlower.Displacement;
            var decel = selectedSlower.Decelerator;

            var a = (displ - 1f) / (displ + 1f);
            var eps = Math.Pow(a, 2);
            var einit = starting * 1e6;
            var results = new List<Tuple<double, double>>();
            var calcluations = Calculate(eps, finishing, einit, decel, displ);

            foreach(var calc in calcluations)
            {
                results.Add(new Tuple<double, double>(calc.Item1, calc.Item2));
            }
            return results;
        }

        Random rand = new Random();

        private List<Tuple<double, double, double>> Calculate(double eps, double Et, double Einit, double decel, double displ)
        {
            var e0 = Einit;
            var x = 0f;
            var y = 0f;
            var result = new List<Tuple<double, double, double>>();
            var gamma = NextFloat(rand);
            while ((gamma <= 0.001) || (1f - gamma <= 0.001))
            {
                gamma = NextFloat(rand);
            }

            var mean = Math.Sqrt(displ);
            var length = NextNormalDistributedVal(mean, mean / 3.5f);
            var cosTheta = 1f - 2f * gamma;
            var cosPsi = (decel * cosTheta + 1f) / Math.Sqrt(decel * decel + 2f * decel * cosTheta + 1f);
            var E1 = (e0 * ((1f + eps) + (1f - eps) * cosTheta)) / 2f;
            var E0 = E1;
            var vert = Math.Sqrt(1 - cosPsi * cosTheta);

            if (NextFloat(rand) > 0)
            {
                x = (float)(x + length * cosPsi);
            }
            else
            {
                x = (float)(x - length * cosPsi);
            }
            result.Add(new Tuple<double, double, double>(x, y, E0));
            //append
            while ((E1 - Et) > 0.0001)
            {
                while ((gamma <= 0.001) || (1f - gamma <= 0.001))
                {
                    gamma = NextFloat(rand);
                }
                mean = Math.Sqrt(decel);
                length = NextNormalDistributedVal(mean, mean / 3.5f);
                cosTheta = 1f - 2f * gamma;
                cosPsi = (decel * cosTheta + 1f) / Math.Sqrt(decel * decel + 2f * decel * cosTheta + 1f);
                E1 = (E0 * ((1f + eps) + (1f - eps) * cosTheta)) / 2f;
                E0 = E1;
                vert = Math.Sqrt(1 - cosPsi * cosTheta);
                if (NextFloat(rand) > 0)
                {
                    x = (float)(x + length * cosPsi);
                }
                else
                {
                    x = (float)(x - length * cosPsi);
                }
                if (NextFloat(rand) > 0)
                {
                    y = (float)(y + length * vert);

                }
                else
                {
                    y = (float)(y - length * vert);

                }
                result.Add(new Tuple<double, double, double>(x, y, E0));
            }
            return result;
        }

        private double NextNormalDistributedVal(double mean, double der)
        {
            var x = getRand();
            var y = getRand();
            var r = Math.Pow(x, 2) + Math.Pow(y, 2);
            while (r >= 1f)
            {
                x = getRand();
                y = getRand();
                r = Math.Pow(x, 2) + Math.Pow(y, 2);
            }
            var z = Math.Sqrt(-2.0 * Math.Log(r) / r);
            return mean + der * y * z;
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
