using System;
using System.Diagnostics;
using System.Windows.Input;

namespace ProjectAirportSim.Helpers
{
	public class RelayCommand<T> : ICommand
	{
		readonly Predicate<T> _canExecute;
		readonly Action<T> _execute;

		public RelayCommand(Action<T> execute)
			: this(execute, null)
		{
		}

		public RelayCommand(Action<T> execute, Predicate<T> canExecute)
		{
			_execute = execute ?? throw new ArgumentNullException("execute");
			_canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged
		{
			add
			{
				if (_canExecute != null)
					CommandManager.RequerySuggested += value;
			}
			remove
			{
				if (_canExecute != null)
					CommandManager.RequerySuggested -= value;
			}
		}

		[DebuggerStepThrough]
		public Boolean CanExecute(Object parameter)
		{
			return _canExecute == null ? true : _canExecute((T)parameter);
		}

		public void Execute(Object parameter)
		{
			_execute((T)parameter);
		}
	}

	public class RelayCommand : ICommand
	{
		readonly Func<Boolean> _canExecute;
		readonly Action _execute;

		public RelayCommand(Action execute)
			: this(execute, null)
		{
		}

		public RelayCommand(Action execute, Func<Boolean> canExecute)
		{
			_execute = execute ?? throw new ArgumentNullException("execute");
			_canExecute = canExecute;
		}

		public event EventHandler CanExecuteChanged
		{
			add
			{

				if (_canExecute != null)
					CommandManager.RequerySuggested += value;
			}
			remove
			{

				if (_canExecute != null)
					CommandManager.RequerySuggested -= value;
			}
		}

		[DebuggerStepThrough]
		public Boolean CanExecute(Object parameter)
		{
			return _canExecute == null ? true : _canExecute();
		}

		public void Execute(Object parameter)
		{
			_execute();
		}
	}
}
