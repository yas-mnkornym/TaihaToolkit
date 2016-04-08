using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Studiotaiha.Toolkit.WPF
{
	public class DelegateCommand<TParam> : NotificationObject, ICommand
	{
		string[] DependanceProperties { get; }

		public DelegateCommand()
		{ }

		public DelegateCommand(IDispatcher dispatcher)
			: base(dispatcher)
		{ }

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
		/// Execute()の処理を行うActionを取得・設定する
		/// </summary>
		public Action<TParam> ExecuteHandler { get; set; }

		/// <summary>
		/// CanExecute()の処理を行うFuncを取得・設定する
		/// </summary>
		public Func<TParam, bool> CanExecuteHandler { get; set; }

		public bool CanExecute(object parameter)
		{
			return (CanExecuteValue = (CanExecuteHandler?.Invoke((TParam)parameter) != false));
		}

		public void Execute(object parameter)
		{
			ExecuteHandler?.Invoke((TParam)parameter);
		}

		public void RaiseCanExecuteChanged()
		{
			Dispatch(() => {
				CanExecuteChanged?.Invoke(this, EventArgs.Empty);
			});
		}

		public void PostCanExecuteChanged()
		{
			BeginDispatch(() => {
				CanExecuteChanged?.Invoke(this, EventArgs.Empty);
			});
		}

		#region CanExecuteValue
		bool canExecuteValue_ = false;
		public bool CanExecuteValue
		{
			get
			{
				return canExecuteValue_;
			}
			set
			{
				if (canExecuteValue_ != value) {
					RaisePropertyChanged();
				}
			}
		}
		#endregion

		public event EventHandler CanExecuteChanged;
	}


	public class DelegateCommand : DelegateCommand<object>
	{
		public DelegateCommand(IDispatcher dispatcher = null)
			: base(dispatcher)
		{ }

		public DelegateCommand(INotifyPropertyChanged owner, params string[] dependanceProperties)
			: base(owner, dependanceProperties)
		{ }
	}
}