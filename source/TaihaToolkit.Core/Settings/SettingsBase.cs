using System;
using System.Runtime.CompilerServices;

namespace Studiotaiha.Toolkit.Settings
{
	/// <summary>
	/// 設定の基本実装を提供するクラス
	/// </summary>
	public abstract class SettingsBase : NotificationObject, IDisposable
	{
		/// <summary>
		/// 設定インスタンスを取得する
		/// </summary>
		ISettingsContainer Container { get; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="container">Instance of ISettingsContainer</param>
		/// <param name="dispatcher">Dispatcher to associate</param>
		protected SettingsBase(
			ISettingsContainer container,
			IDispatcher dispatcher = null)
			: base(dispatcher)
		{
			if (container == null) { throw new ArgumentNullException("settings"); }

			Container = container;
			Container.SettingChanged += Container_SettingChanged;
		}

		void Container_SettingChanged(object sender, SettingChangeEventArgs e)
		{
			RaisePropertyChanged(e.Key);
		}

		/// <summary>
		/// Gets a setting value.
		/// </summary>
		/// <typeparam key="T">Type of the value</typeparam>
		/// <param key="key">Key of the setting</param>
		/// <param key="defaultValue">Default value</param>
		/// <returns>Decrypted value, or default value (if the key doesn't exist.)</returns>
		protected T GetMe<T>(T defaultValue = default(T), [CallerMemberName]string key = null)
		{
			return Container.Get(key, defaultValue);
		}

		/// <summary>
		/// Sets a setting value.
		/// </summary>
		/// <typeparam key="T">Type of the value</typeparam>
		/// <param key="key">Key of the setting</param>
		/// <param key="value">Value</param>
		protected void SetMe<T>(T value, [CallerMemberName]string key = null)
		{
			Container.Set(key, value);
		}
		
		/// <summary>
		/// Gets a decrypted setting value.
		/// </summary>
		/// <param key="defaultValue">Default value</param>
		/// <param key="key">Key of the setting</param>
		/// <returns>Decrypted value, or default value (if the key doesn't exist.)</returns>
		protected string GetDecryptedOrDefault(string defaultValue = null, [CallerMemberName]string key = null)
		{
			return Container.GetDecryptedStringOrDefault(key, defaultValue);
		}

		/// <summary>
		/// Sets an encrypted setting value.
		/// </summary>
		/// <param key="key">Key of the setting</param>
		/// <param key="value">The value</param>
		protected void SetMeCrypted(string value, [CallerMemberName]string key = null)
		{
			Container.SetCryptedString(key, value);
		}

		/// <summary>
		/// Remove a setting.
		/// </summary>
		/// <param key="key">Key of the settings</param>
		protected void RemoveMe([CallerMemberName]string key = null)
		{
			Container.Remove(key);
		}

		/// <summary>
		/// Sets a setting value.
		/// </summary>
		/// <typeparam key="T">Type of the value</typeparam>
		/// <param key="key">Key of the setting</param>
		/// <param key="value">Value</param>
		protected void Set<T>(string key, T value)
		{
			Container.Set(key, value);
		}

		/// <summary>
		/// Gets a setting value.
		/// </summary>
		/// <typeparam key="T">Type of the value</typeparam>
		/// <param key="key">Key of the setting</param>
		/// <param key="defaultValue">Default value</param>
		/// <returns>Decrypted value, or default value (if the key doesn't exist.)</returns>
		protected T Get<T>(string key, T defaultValue = default(T))
		{
			return Container.Get(key, defaultValue);
		}

		/// <summary>
		/// Sets an encrypted setting value.
		/// </summary>
		/// <param key="key">Key of the setting</param>
		/// <param key="value">Value</param>
		protected void SetCrypted(string key, string value)
		{
			Container.SetCryptedString(key, value);
		}

		/// <summary>
		/// Gets a decrypted setting value.
		/// </summary>
		/// <param key="key">Key of the setting</param>
		/// <param key="defaultValue">Default value</param>
		/// <returns>Decrypted value, or default value (if the key doesn't exist.)</returns>
		protected string GetDecrypted(string key, string defaultValue = null)
		{
			return Container.GetDecryptedStringOrDefault(key, defaultValue);
		}

		/// <summary>
		/// Remove a setting.
		/// </summary>
		/// <param key="key">Key of the settings</param>
		protected void Remove(string key)
		{
			Container.Remove(key);
		}

		/// <summary>
		/// Remove all settings.
		/// </summary>
		protected void Clear()
		{
			Container.Clear();
		}

		#region IDisposable interface

		bool isDisposed_ = false;
		virtual protected void Dispose(bool disposing)
		{
			if (isDisposed_) { return; }
			if (disposing) {
				Container.SettingChanged -= Container_SettingChanged;
			}
			isDisposed_ = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion
	}
}
