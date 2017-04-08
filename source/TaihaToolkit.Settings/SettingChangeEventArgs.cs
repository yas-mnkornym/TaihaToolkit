using System;

namespace Studiotaiha.Toolkit.Settings
{
	public sealed class SettingChangeEventArgs : EventArgs
	{
		public SettingChangeEventArgs(
			string tag,
			string key,
			object oldValue,
			object newValue)
		{
			Tag = tag;
			Key = key;
			OldValue = oldValue;
			NewValue = newValue;
		}

		/// <summary>
		/// Gets tag of the setting container.
		/// </summary>
		public string Tag { get; }

		/// <summary>
		/// Gets key of the setting.
		/// </summary>
		public string Key { get; }

		/// <summary>
		/// Gets the old value.
		/// </summary>
		public object OldValue { get; }

		/// <summary>
		/// Gets the new value.
		/// </summary>
		public object NewValue { get; }
	}
}
