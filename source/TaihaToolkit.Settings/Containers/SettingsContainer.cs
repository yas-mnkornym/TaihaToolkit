using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Studiotaiha.Toolkit.Composition;

namespace Studiotaiha.Toolkit.Settings.Containers
{
	[ComponentImplementation(typeof(ISettingsContainer), int.MaxValue)]
	public sealed class SettingsContainer : NotificationObjectWithPropertyBag, ISettingsContainer
	{
		Dictionary<string, ISettingsContainer> ChildrenMap { get; } = new Dictionary<string, ISettingsContainer>();

		public IEnumerable<string> ChildContainerTags => ChildrenMap.Keys;

		public IEnumerable<string> Keys => PropertyBag.Keys;

		public IEnumerable<KeyValuePair<string, object>> Settings => PropertyBag;

		public string Tag { get; }

		public SettingsContainer(string tag)
		{
			if (tag == null) { throw new ArgumentNullException(nameof(tag)); }
			Tag = tag.Replace("\\", "-");
		}


		public bool Exists(string key)
		{
			return PropertyBag.ContainsKey(key);
		}

		public void Set<T>(string key, T value)
		{
			SetValue(
				value,
				actBeforeChange: (oldValue, newValue) => {
					SettingChanging?.Invoke(this, new SettingChangeEventArgs(Tag, key, oldValue, newValue));
				},
				actAfterChange: (oldValue, newValue) => {
					SettingChanged?.Invoke(this, new SettingChangeEventArgs(Tag, key, oldValue, newValue));
				},
				propertyName: key);
		}

		public T Get<T>(string key, T defaultValue = default(T))
		{
			return GetValue(defaultValue, key);
		}

		public string GetDecryptedStringOrDefault(string key, string defaultValue = null)
		{
			throw new NotSupportedException();
		}

		public void SetCryptedString(string key, string value)
		{
			throw new NotSupportedException();
		}

		public bool Remove(string key)
		{
			return PropertyBag.Remove(key);
		}
		public void Clear()
		{
			var keysToRemove = Keys
				.Where(x => !x.Contains("\\"))
				.ToArray();

			foreach (var key in keysToRemove) {
				Remove(key);
			}
		}

		public ISettingsContainer GetOrCreateChildContainer(string tag)
		{
			if (tag == null) { throw new ArgumentNullException(nameof(tag)); }

			ISettingsContainer child;
			if (!ChildrenMap.TryGetValue(tag, out child)) {
				child = new ProxySettingsContainer(this, tag);
				ChildrenMap[tag] = child;
			}

			return child;
		}

		public void RemoveChildContainer(string tag)
		{
			ISettingsContainer child;
			if (ChildrenMap.TryGetValue(tag, out child)) {
				child.Clear();
				ChildrenMap.Remove(tag);

				if (child is IDisposable) {
					((IDisposable)child).Dispose();
				}
			}
		}

		public event EventHandler<SettingChangeEventArgs> SettingChanged;
		public event EventHandler<SettingChangeEventArgs> SettingChanging;
	}
}
