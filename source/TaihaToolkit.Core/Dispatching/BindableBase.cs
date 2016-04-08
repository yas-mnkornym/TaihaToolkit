using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Studiotaiha.Toolkit
{
	[DataContract]
	public class BindableBase : NotificationObject
	{
		[IgnoreDataMember]
		Dictionary<string, object> PropertyBag { get; } = new Dictionary<string, object>();

		public BindableBase(IDispatcher dispatcher = null)
			: base(dispatcher)
		{ }

		/// <summary>
		/// プロパティの値を設定する。
		/// </summary>
		/// <typeparam name="T">変数の型</typeparam>
		/// <param name="dst">設定する対象の変数への参照</param>
		/// <param name="value">設定する値</param>
		/// <param name="propertyName">プロパティ名</param>
		protected virtual bool SetValue<T>(
			ref T dst,
			T value,
			[CallerMemberName]string propertyName = null)
		{
			var isChanged = dst == null
				? value != null
				: !dst.Equals(value);

			if (isChanged) {
				dst = value;
				RaisePropertyChanged(propertyName);
				return true;
			}

			return false;
		}

		/// <summary>
		/// プロパティの値を設定する。
		/// </summary>
		/// <typeparam name="T">変数の型</typeparam>
		/// <param name="dst">設定する対象の変数への参照</param>
		/// <param name="value">設定する値</param>
		/// <param name="actBeforeChange">値が変更される前に実行する処理</param>
		/// <param name="actAfterChange">値が変更された後に実行される処理</param>
		/// <param name="propertyName">プロパティ名</param>
		/// <returns>値が変更されたらtrue, されなければfalse</returns>
		/// <remarks>actBeforeChange, actAfterChangeは、値が変更される時のみ実行される。</remarks>
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

		protected virtual bool SetValue<T>(
			T value,
			Action<T> actBeforeChange = null,
			Action<T> actAfterChange = null,
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
				actBeforeChange?.Invoke(oldValue);

				PropertyBag[propertyName] = value;

				actAfterChange?.Invoke(value);
				RaisePropertyChanged(propertyName);
			}

			return isChanged;
		}

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
