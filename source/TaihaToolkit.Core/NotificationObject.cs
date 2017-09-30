using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Studiotaiha.Toolkit
{
	[DataContract]
	public class NotificationObject : Dispatchable, INotifyPropertyChanged
	{
		public bool EnableAutoDispatch { get; set; }

		public NotificationObject(IDispatcher dispatcher = null)
			: base(dispatcher)
		{ }

		public NotificationObject(IDispatcher dispatcher, bool enableAutoDispatch)
			: base(dispatcher)
		{
			EnableAutoDispatch = enableAutoDispatch;
		}

		/// <summary>
		/// Sets the value to the variable and trigger property changed event.
		/// </summary>
		/// <typeparam name="T">Type of the value</typeparam>
		/// <param name="dst">Target variable to set the value</param>
		/// <param name="value">Value to be set</param>
		/// <param name="actBeforeChange">Action object that will be invoked before the value is changed</param>
		/// <param name="actAfterChange">Action object that will be invoked before the value is changed</param>
		/// <param name="propertyName">Name of the property</param>
		/// <returns>True if the value is changed.</returns>
		protected virtual bool SetValue<T>(
			ref T dst,
			T value,
			Action<T> actBeforeChange = null,
			Action<T> actAfterChange = null,
			[CallerMemberName]string propertyName = null)
		{
			var isChanged = dst == null
				? value != null
				: !dst.Equals(value);

			if (isChanged) {
				actBeforeChange?.Invoke(dst);
				dst = value;
				actAfterChange?.Invoke(dst);
				RaisePropertyChanged(propertyName);
				return true;
			}

			return false;
		}

		/// <summary>
		/// Notify that a property value is changed.
		/// </summary>
		/// <param name="propertyName"></param>
		protected virtual void RaisePropertyChanged([CallerMemberName]string propertyName = null)
		{
			if (PropertyChanged != null) {
				if (EnableAutoDispatch) {
					Dispatch(() => {
						PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
					});
				}
				else {
					PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
				}
			}
		}

		/// <summary>
		/// -Gets a member name from an exception syntax.
		/// </summary>
		/// <typeparam name="MemberType">メンバの型</typeparam>
		/// <param name="expression">式</param>
		/// <returns>メンバ名</returns>
		public string GetMemberName<MemberType>(Expression<Func<MemberType>> expression)
		{
			return ((MemberExpression)expression.Body).Member.Name;
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
