using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMFPCommonDataStructures;
using MMFPSoftwareSystem.Views.Windows;

namespace MMFPSoftwareSystem
{
    class StudentSelectionViewModel
    {
        public Command OkCommand => _OkCommand ?? (_OkCommand = new Command(Ok));

        public Command CancelCommand => _cancelCommand ?? (_cancelCommand = new Command(Cancel));

        private void Cancel(object window)
        {
            var dialog = window as StudentSelectionWindow;
            dialog.DialogResult = false;
            dialog.Close();
        }

        private void Ok(object window)
        {
            var dialog = window as StudentSelectionWindow;
            dialog.DialogResult = true;
            dialog.Close();
        }


        private ObservableCollection<Group> _groups;

        public StudentSelectionViewModel(GroupSet groups)
        {
            if (groups != null && groups.Groups != null)
            {
                Groups = groups.Groups;
                SelectedGroup = Groups.FirstOrDefault();
                SelectedStudent = SelectedGroup.Students.FirstOrDefault();
            }
        }

        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set
            {
                if (_groups == value) return;
                _groups = value;

                OnPropertyChanged(nameof(Groups));
            }
        }

        private ObservableCollection<Student> _students;

        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set
            {
                if (_students == value) return;
                _students = value;
                OnPropertyChanged(nameof(Students));
            }
        }

        private Group _selectedGroup;

        public Group SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                if (_selectedGroup == value) return;
                _selectedGroup = value;
                Students = value.Students;
                OnPropertyChanged(nameof(SelectedGroup));
            }
        }

        private Student _selectedStudent;
        private Command _OkCommand;
        private Command _cancelCommand;

        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                if (_selectedStudent == value) return;
                _selectedStudent = value;
                OnPropertyChanged(nameof(SelectedStudent));
            }
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
