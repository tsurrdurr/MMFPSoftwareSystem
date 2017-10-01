using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MMFPSoftwareSystem.Models;
using MMFPSoftwareSystem.Views;

namespace MMFPSoftwareSystem
{
    public class MainViewModel : INotifyPropertyChanged
    {


        public MainViewModel()
        {
            FillTopicsList();
            BindViewModels(CurrentTopic);
        }

        public IModelingControlsViewModel modelingControlsViewModel => _modelingControlsViewModel;
        public IGraphViewModel graphViewModel => _graphViewModel;
        public ITheoryViewModel theoryViewModel => _theoryViewModel;
        public ITestingViewModel testingViewModel => _testingViewModel;

        public List<Models.Topic> TopicsList => _topicsList ?? (_topicsList = new List<Topic>());

        public Models.Topic CurrentTopic
        {
            get { return _currentTopic ?? (_currentTopic = new Topic()); }
            set
            {
                if (_currentTopic != value)
                {
                    _currentTopic = value;
                    OnPropertyChanged(nameof(CurrentTopic));
                    BindViewModels(CurrentTopic);
                }
            }
        }

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




        private void FillTopicsList()
        {
            var topic = CreatePredefinedTopic();
            _topicsList = new List<Models.Topic> { topic };
            _currentTopic = _topicsList.FirstOrDefault();
            OnPropertyChanged(nameof(TopicsList));
        }

        private Topic CreatePredefinedTopic()
        {
            var name = "Исследование процесса замедления нейтронов";
            var topic = new Models.Topic(name, theoryViewModel, modelingControlsViewModel, graphViewModel, testingViewModel);
            return topic;
        }

        private void BindViewModels(Topic currentTopic)
        {
            _modelingControlsViewModel = currentTopic.modelingControls;
            _graphViewModel = currentTopic.graph;
            _theoryViewModel = currentTopic.theory;
            _testingViewModel = currentTopic.testing;
            OnPropertyChanged(nameof(modelingControlsViewModel));
            OnPropertyChanged(nameof(graphViewModel));
            OnPropertyChanged(nameof(theoryViewModel));
            OnPropertyChanged(nameof(testingViewModel));
        }

        private IModelingControlsViewModel _modelingControlsViewModel;
        private IGraphViewModel _graphViewModel;
        private ITheoryViewModel _theoryViewModel;
        private ITestingViewModel _testingViewModel;
        private AdminViewModel _adminViewModel;
        private HelpViewModel _helpViewModel;
        private List<Models.Topic> _topicsList;

        private Command _openAdminCommand;
        private Command _openHelpCommand;
        private Models.Topic _currentTopic;

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
