using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Studiotaiha.Toolkit
{
	[DataContract]
	public class NotificationObjectWithPropertyBag : NotificationObject
	{
		[IgnoreDataMember]
		protected IDictionary<string, object> PropertyBag { get; } = new Dictionary<string, object>();

		public NotificationObjectWithPropertyBag(IDispatcher dispatcher = null)
			: base(dispatcher)
		{ }

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
			Action<T> actBeforeChange,
			Action<T> actAfterChange,
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
		/// Set a value to the property bag.
		/// </summary>
		/// <typeparam name="T">Type of the value.</typeparam>
		/// <param name="value">The value to be set.</param>
		/// <param name="actBeforeChange">Action object that will be invoked before the value is changed</param>
		/// <param name="actAfterChange">Action object that will be invoked before the value is changed</param>
		/// <param name="propertyName">Name of the property</param>
		/// <returns>True if the value is changed.</returns>
		protected virtual bool SetValue<T>(
			T value,
			Action<T, T> actBeforeChange = null,
			Action<T, T> actAfterChange = null,
			[CallerMemberName]string propertyName = null)
		{
			if (propertyName == null) { throw new ArgumentNullException(nameof(propertyName)); }

			object oldValueObject;
			PropertyBag.TryGetValue(propertyName, out oldValueObject);

			var oldValue = oldValueObject is T
				? (T)oldValueObject
				: default(T);

			var isChanged = value == null
				? oldValueObject == null
				: !value.Equals(oldValue);

			if (isChanged) {
				actBeforeChange?.Invoke(oldValue, value);

				PropertyBag[propertyName] = value;

				actAfterChange?.Invoke(oldValue, value);
				RaisePropertyChanged(propertyName);
			}

			return isChanged;
		}

		/// <summary>
		/// Gets a value from the property bag.
		/// </summary>
		/// <typeparam name="T">Type of the value.</typeparam>
		/// <param name="defaultValue">Default value to be returned the proeprty is not stored in the bag</param>
		/// <param name="propertyName">Name of the property</param>
		/// <returns>The value or default value.</returns>
		protected virtual T GetValue<T>(
			T defaultValue = default(T),
			[CallerMemberName]string propertyName = null)
		{
			if (propertyName == null) { throw new ArgumentNullException(nameof(propertyName)); }

			object oldValueObject;
			PropertyBag.TryGetValue(propertyName, out oldValueObject);

			return oldValueObject is T
				? (T)oldValueObject
				: defaultValue;
		}
	}
}
