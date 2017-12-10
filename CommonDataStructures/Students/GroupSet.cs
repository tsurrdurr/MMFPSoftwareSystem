using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMFPCommonDataStructures
{
    public class GroupSet : INotifyPropertyChanged
    {
        public DateTime CreatedAt;
        public DateTime ModifiedAt;

        public ObservableCollection<Group> Groups
        {
            get { return _groups ?? new ObservableCollection<Group>(); }
            set
            {
                if (_groups == value) return;
                _groups = value;
                OnPropertyChanged(nameof(Groups));
            }
        }

        private ObservableCollection<Group> _groups;

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
