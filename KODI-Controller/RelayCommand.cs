using System;
using System.Windows.Input;

namespace KODI_Controller
{
    internal class RelayCommand : ICommand
    {

        private Action _execute;
        private Func<bool> _canExecute;


        public RelayCommand(Action executeAction) : this(executeAction, () => true) { }

        public RelayCommand(Action executeAction, Func<bool> canExecutePredicate)
        {
            if (executeAction == null)
                throw new ArgumentNullException("execute");

            _execute = executeAction;
            _canExecute = canExecutePredicate;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }


    internal class RelayCommand<T> : ICommand
    {

        private static bool DefaultCanExecute(T item) { return true; }


        private Action<T> _execute;
        private Predicate<T> _canExecute;


        public RelayCommand(Action<T> executeAction, Predicate<T> canExecutePredicate)
        {
            if (executeAction == null)
                throw new ArgumentNullException("execute");

            _execute = executeAction;
            _canExecute = canExecutePredicate;
        }

        public RelayCommand(Action<T> executeAction) : this(executeAction, DefaultCanExecute) { }


        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : parameter is T ? _canExecute((T)parameter) : false;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }

}
