using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using MMFPCommonDataStructures;
using MMFPSoftwareSystem.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MMFPSoftwareSystem
{
    public class MainViewModel : INotifyPropertyChanged
    {

        public MainViewModel()
        {
            FillTopicsList();
            BindViewModels(CurrentTopic);
            theoryViewModel.OpenDocument(AppDomain.CurrentDomain.BaseDirectory + @"Resources\HelpPages\table of contents.html");
        }

        public IModelingControlsViewModel modelingControlsViewModel => _modelingControlsViewModel;
        public IGraphViewModel graphViewModel => _graphViewModel;
        public ITheoryViewModel theoryViewModel => _theoryViewModel;
        public ITestingViewModel testingViewModel => _testingViewModel;

        public List<Topic> TopicsList => _topicsList ?? (_topicsList = new List<Topic>());

        public Topic CurrentTopic
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
        public Command ExportModelingSettingsCommand => _exportModelingSettingsCommand ?? (_exportModelingSettingsCommand = new Command(ExportModelingSettings));
        public Command ImportModelingSettingsCommand => _importModelingSettingsCommand ?? (_importModelingSettingsCommand = new Command(ImportModelingSettings));

        private void ImportModelingSettings()
        {
            try
            {
                var dialog = new OpenFileDialog
                {
                    Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*"
                };
                if (dialog.ShowDialog() == true)
                {
                    var json = File.ReadAllText(dialog.FileName);
                    IterateThroughProperties(json);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        private void IterateThroughProperties(string json)
        {
            JObject obj = JObject.Parse(json);
            foreach (var pair in obj)
            {
                var propertiesInfo = modelingControlsViewModel
                    .GetType()
                    .GetProperties();

                if (propertiesInfo.Any(x =>
                    x.Name == pair.Key.ToString()))
                {
                    var targetPropertyInfo = propertiesInfo.Single(x => x.Name == pair.Key.ToString());
                    targetPropertyInfo.SetValue(modelingControlsViewModel, pair.Value.ToString());
                }
            }
        }

        private void ExportModelingSettings()
        {
            try
            {
                var jsonSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
                string output = JsonConvert.SerializeObject(modelingControlsViewModel, jsonSettings);
                var dialog = new SaveFileDialog
                {
                    FileName = CurrentTopic.ToString() + " modeling settings",
                    Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",

                };
                if (dialog.ShowDialog() == true)
                {
                    File.WriteAllText(dialog.FileName, output);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

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
            _topicsList = new List<Topic> { topic };
            _currentTopic = _topicsList.FirstOrDefault();
            OnPropertyChanged(nameof(TopicsList));
        }

        private Topic CreatePredefinedTopic()
        {
            var name = "Исследование процесса замедления нейтронов";
            var graphVM = new GraphViewModel();
            var topic = new Topic(name, new TheoryViewModel(), new ModelingControlsViewModel(graphVM), graphVM, new TestingViewModel());
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
        private List<Topic> _topicsList;

        private Command _openAdminCommand;
        private Command _openHelpCommand;
        private Topic _currentTopic;
        private Command _exportModelingSettingsCommand;
        private Command _importModelingSettingsCommand;

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
