using System;
using System.Collections.Generic;

namespace Studiotaiha.Toolkit.Settings
{
	public interface ISettingsContainer
	{
		/// <summary>
		/// Gets the container tag.
		/// </summary>
		string Tag { get; }
		
		/// <summary>
		/// Sets a setting value.
		/// </summary>
		/// <typeparam key="T">Type of the value</typeparam>
		/// <param key="key">Key of the setting</param>
		/// <param key="value">Value</param>
		void Set<T>(string key, T value);

		/// <summary>
		/// Gets a setting value.
		/// </summary>
		/// <typeparam key="T">Type of the value</typeparam>
		/// <param key="key">Key of the setting</param>
		/// <param key="defaultValue">Default value</param>
		/// <returns>Decrypted value, or default value (if the key doesn't exist.)</returns>
		T Get<T>(string key, T defaultValue = default(T));

		/// <summary>
		/// Sets an encrypted setting value.
		/// </summary>
		/// <param key="key">Key of the setting</param>
		/// <param key="value">Value</param>
		void SetCryptedString(string key, string value);


		/// <summary>
		/// Gets a decrypted setting value.
		/// </summary>
		/// <param key="key">Key of the setting</param>
		/// <param key="defaultValue">Default value</param>
		/// <returns>Decrypted value, or default value (if the key doesn't exist.)</returns>
		string GetDecryptedStringOrDefault(string key, string defaultValue = null);

		/// <summary>
		/// Verify whether the setting exists or not.
		/// </summary>
		/// <param key="key">Key of the setting</param>
		/// <returns>True if the setting exists. False otherwise.</returns>
		bool Exists(string key);

		/// <summary>
		/// Remove a setting.
		/// </summary>
		/// <param key="key">Key of the settings</param>
		bool Remove(string key);

		/// <summary>
		/// Remove all settings.
		/// </summary>
		void Clear();

		/// <summary>
		/// Gets all keys.
		/// </summary>
		IEnumerable<string> Keys { get; }

		/// <summary>
		/// Gets all settings.
		/// </summary>
		IEnumerable<KeyValuePair<string, object>> Settings { get; }

		/// <summary>
		/// Gets or create child container.
		/// </summary>
		/// <param name="tag">Child container tag</param>
		/// <param name="knownTypes">Known types for DataContractSerializer.</param>
		/// <remarks>If the child settings has same tag already exists, this method returns the same instance.</remarks>
		/// <returns>Child container</returns>
		ISettingsContainer GetOrCreateChildContainer(string tag);

		/// <summary>
		/// Remove child settings.
		/// </summary>
		/// <param name="tag">Tag of the child container to be removed</param>
		void RemoveChildContainer(string tag);

		/// <summary>
		/// Gets container tags of children.
		/// </summary>
		IEnumerable<string> ChildContainerTags { get; }

		/// <summary>
		/// Notifies before a setting is changed.
		/// </summary>
		event EventHandler<SettingChangeEventArgs> SettingChanging;

		/// <summary>
		/// Notifies after a setting is changed.
		/// </summary>
		event EventHandler<SettingChangeEventArgs> SettingChanged;
	}
}
