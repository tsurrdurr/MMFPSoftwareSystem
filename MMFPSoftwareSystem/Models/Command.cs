using System;
using System.Windows.Input;

namespace MMFPSoftwareSystem
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private bool _canExecute = true;
        private Action<object> _action;

        public Command(Action action, bool canExecute = true)
        {
            _action = (x) => action();
            _canExecute = canExecute;
        }
        public Command(Action<object> action, bool canExecute = true)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute
        {
            get { return _canExecute; }
            set
            {
                if (_canExecute != value)
                {
                    _canExecute = value;
                    OnCanExecuteChanged();
                }
            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute;
        }

        public void Execute(object parameter)
        {
            _action.Invoke(parameter);
        }

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}