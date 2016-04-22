using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Studiotaiha.Toolkit
{
	public class DelegateCommand<TParam> : NotificationObject, ICommand
	{
		string[] DependanceProperties { get; }

		/// <summary>
		/// Constructor
		/// </summary>
		public DelegateCommand()
		{ }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="dispatcher">dispatcher object to be associated to the command</param>
		public DelegateCommand(IDispatcher dispatcher)
			: base(dispatcher)
		{ }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="owner">object to watch</param>
		/// <param name="dependanceProperties">names of properties to be watched</param>
		/// <remarks>RaiseCanExecute() is automatically called when any watched property is changed.</remarks>
		public DelegateCommand(INotifyPropertyChanged owner, params string[] dependanceProperties)
		{
			if (dependanceProperties == null) { throw new ArgumentNullException(nameof(dependanceProperties)); }
			DependanceProperties = dependanceProperties;
			owner.PropertyChanged += Owner_PropertyChanged;
		}

		private void Owner_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (DependanceProperties.Contains(e.PropertyName)) {
				RaiseCanExecuteChanged();
			}
		}

		/// <summary>
		/// Gets or sets Action object handles Execute().
		/// </summary>
		public Action<TParam> ExecuteHandler { get; set; }

		/// <summary>
		/// Gets or sets Func object handles CanExecute().
		/// </summary>
		public Func<TParam, bool> CanExecuteHandler { get; set; }

		#region ICommand interface

		public bool CanExecute(object parameter)
		{
			return (CanExecuteValue = (CanExecuteHandler?.Invoke((TParam)parameter) != false));
		}

		public void Execute(object parameter)
		{
			ExecuteHandler?.Invoke((TParam)parameter);
		}

		public event EventHandler CanExecuteChanged;

		#endregion

		/// <summary>
		/// Raise CanExecuteChanged event 
		/// </summary>
		public void RaiseCanExecuteChanged()
		{
			Dispatch(() => {
				CanExecuteChanged?.Invoke(this, EventArgs.Empty);
			});
		}

		/// <summary>
		/// Raise CanExecuteChanged event in UI thread, using associated dispatcher.
		/// </summary>
		/// <remarks>
		/// if Dispatcher is null, this method doesn't grant that the event is triggered in UI thread.
		/// </remarks>
		public void PostCanExecuteChanged()
		{
			BeginDispatch(() => {
				CanExecuteChanged?.Invoke(this, EventArgs.Empty);
			});
		}

		#region CanExecuteValue
		bool canExecuteValue_ = false;

		/// <summary>
		/// Gets the latest CanExecute() value
		/// </summary>
		public bool CanExecuteValue
		{
			get
			{
				return canExecuteValue_;
			}
			private set
			{
				if (canExecuteValue_ != value) {
					RaisePropertyChanged();
				}
			}
		}
		#endregion
	}

	public class DelegateCommand : DelegateCommand<object>
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="dispatcher">dispatcher object to be associated to the command</param>
		public DelegateCommand(IDispatcher dispatcher = null)
			: base(dispatcher)
		{ }
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="owner">object to watch</param>
		/// <param name="dependanceProperties">names of properties to be watched</param>
		/// <remarks>RaiseCanExecute() is automatically called when any watched property is changed.</remarks>
		public DelegateCommand(INotifyPropertyChanged owner, params string[] dependanceProperties)
			: base(owner, dependanceProperties)
		{ }
	}
}