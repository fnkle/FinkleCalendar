using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CalendarApp
{
    public class AppCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public AppCommand(Action execute)
        {
            _execute = execute;
            _canExecute = () => true;
        }

        public AppCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (_canExecute == null || _execute == null)
                return false;
            return _canExecute();
        }

        public void Execute(object? parameter)
        {
            if (_canExecute())
                _execute.Invoke();
        }
    }
}