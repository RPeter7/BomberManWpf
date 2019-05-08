using System;
using System.Diagnostics;
using System.Windows.Input;

namespace BomberMan.View.Common
{
    public class ParameterizedRelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public ParameterizedRelayCommand(Action<object> execute) : this(execute, null) { }

        public ParameterizedRelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Execute(object parameter) => _execute(parameter);
    }
}
