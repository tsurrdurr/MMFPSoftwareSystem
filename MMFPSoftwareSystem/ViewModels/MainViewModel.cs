using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MMFPSoftwareSystem.Views;

namespace MMFPSoftwareSystem
{
    public class MainViewModel : INotifyPropertyChanged
    {


        public MainViewModel()
        {
            
        }

        public ModelingControlsViewModel modelingControlsViewModel => _modelingControlsViewModel ?? (_modelingControlsViewModel = new ModelingControlsViewModel());
        public GraphViewModel graphViewModel => _graphViewModel ?? (_graphViewModel = new GraphViewModel());
        public TheoryViewModel theoryViewModel => _theoryViewModel ?? (_theoryViewModel = new TheoryViewModel());
        public TestingViewModel testingViewModel => _testingViewModel ?? (_testingViewModel = new TestingViewModel());
        
        public Command SampleCommand => _sampleCommand ?? (_sampleCommand = new Command(SampleMethod));
        public Command OpenAdminCommand => _openAdminCommand ?? (_openAdminCommand = new Command(OpenAdmin));
        public Command OpenHelpCommand => _openHelpCommand ?? (_openHelpCommand = new Command(OpenHelp));
        
        private void OpenAdmin()
        {
            var adminWindow = new AdminView
            {
                DataContext = _adminViewModel ?? (_adminViewModel = new AdminViewModel()),
                Owner = Application.Current.MainWindow
            };
            adminWindow.Show();
        }

        private void OpenHelp()
        {
            var helpWindow = new HelpView
            {
                DataContext = _helpViewModel ?? (_helpViewModel = new HelpViewModel()),
                Owner = Application.Current.MainWindow
            };
            helpWindow.Show();
        }


        public string Test
        {
            get => "test";
            set { OnPropertyChanged(nameof(Test)); }
        }


        private ModelingControlsViewModel _modelingControlsViewModel;
        private GraphViewModel _graphViewModel;
        private TheoryViewModel _theoryViewModel;
        private TestingViewModel _testingViewModel;
        private AdminViewModel _adminViewModel;
        private HelpViewModel _helpViewModel;

        private Command _sampleCommand;
        private Command _openAdminCommand;
        private Command _openHelpCommand;

        private void SampleMethod()
        {
            Debug.Write("method");
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
