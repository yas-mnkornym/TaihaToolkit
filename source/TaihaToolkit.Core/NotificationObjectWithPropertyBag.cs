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
		protected IDictionary<string, object> PropertyBag { get; private set; } = new Dictionary<string, object>();

		public NotificationObjectWithPropertyBag(IDispatcher dispatcher = null)
			: base(dispatcher)
		{ }

		public NotificationObjectWithPropertyBag(IDispatcher dispatcher, bool enableAutoDispatch)
			: base(dispatcher, enableAutoDispatch)
		{
		}

	[OnDeserializing]
		void OnDeserializingForNotificationObjectWithPropertyBag(StreamingContext context)
		{
			PropertyBag = new Dictionary<string, object>();
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
			var ret = PropertyBag.TryGetValue(propertyName, out oldValueObject);

			var oldValue = ret
				? (T)oldValueObject
				: default(T);

			var isChanged = !ret
				? true
				: value == null
					? oldValueObject != null
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

			object valueObject;
			if (PropertyBag.TryGetValue(propertyName, out valueObject)) {
				return (T)valueObject;
			}

			return defaultValue;
		}
	}
}
