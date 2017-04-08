using System;
using System.Collections.Generic;
using System.Linq;

namespace Studiotaiha.Toolkit.Settings.Containers
{
	class ProxySettingsContainer : ISettingsContainer, IDisposable
	{
		ISettingsContainer ParentContainer { get; }	
		Dictionary<string, ISettingsContainer> Children { get; } = new Dictionary<string, ISettingsContainer>();
		
		string ParentTagPrefix { get; }
		string TagPrefix => string.Format(@"{0}__{1}__", ParentTagPrefix, Tag);
		string KeyPrefix => TagPrefix + "\\";

		public IEnumerable<string> ChildContainerTags => Children.Keys;

		public IEnumerable<string> Keys => Settings.Select(x => x.Key);

		public IEnumerable<KeyValuePair<string, object>> Settings => ParentContainer.Settings.Where(x => x.Key.StartsWith(KeyPrefix));

		public string Tag { get; }

		public event EventHandler<SettingChangeEventArgs> SettingChanged;
		public event EventHandler<SettingChangeEventArgs> SettingChanging;

		public ProxySettingsContainer(
			ISettingsContainer parent,
			string tag)
			: this(parent, tag, parent.Tag)
		{ }

		private ProxySettingsContainer(
			ISettingsContainer parent,
			string tag,
			string parentTagPrefix)
		{
			if (parent == null) { throw new ArgumentNullException(nameof(parent)); }
			if (tag == null) { throw new ArgumentNullException(nameof(tag)); }
			if (parentTagPrefix == null) { throw new ArgumentNullException(nameof(parentTagPrefix)); }

			ParentContainer = parent;
			Tag = tag.Replace("\\", "-");
			ParentTagPrefix = parentTagPrefix;

			ParentContainer.SettingChanging += ParentContainer_SettingChanging;
			ParentContainer.SettingChanged += ParentContainer_SettingChanged;
		}

		private void ParentContainer_SettingChanged(object sender, SettingChangeEventArgs e)
		{
			if (e.Key.StartsWith(KeyPrefix)) {
				var rawKey = e.Key.Substring(KeyPrefix.Length);
				SettingChanging?.Invoke(this, new SettingChangeEventArgs(Tag, rawKey, e.OldValue, e.NewValue));
			}
		}

		private void ParentContainer_SettingChanging(object sender, SettingChangeEventArgs e)
		{
			if (e.Key.StartsWith(KeyPrefix)) {
				var rawKey = e.Key.Substring(KeyPrefix.Length);
				SettingChanged?.Invoke(this, new SettingChangeEventArgs(Tag, rawKey, e.OldValue, e.NewValue));
			}
		}

		public void Clear()
		{
			var keysToRemove = Keys.ToArray();
			foreach (var key in keysToRemove) {
				ParentContainer.Remove(key);
			}
		}

		public bool Exists(string key)
		{
			if (key == null) { throw new ArgumentNullException(nameof(key)); }
			return ParentContainer.Exists(KeyPrefix + key);
		}

		public T Get<T>(string key, T defaultValue = default(T))
		{
			if (key == null) { throw new ArgumentNullException(nameof(key)); }
			return ParentContainer.Get(KeyPrefix + key, defaultValue);
		}

		public string GetDecryptedStringOrDefault(string key, string defaultValue = null)
		{
			if (key == null) { throw new ArgumentNullException(nameof(key)); }
			return ParentContainer.Get(KeyPrefix + key, defaultValue);
		}

		public ISettingsContainer GetOrCreateChildContainer(string tag)
		{
			if (tag == null) { throw new ArgumentNullException(nameof(tag)); }

			ISettingsContainer child;
			if (!Children.TryGetValue(tag, out child)) {
				child = new ProxySettingsContainer(this, tag, TagPrefix);
				Children[tag] = child;
			}

			return child;
		}

		public bool Remove(string key)
		{
			if (key == null) { throw new ArgumentNullException(nameof(key)); }
			return ParentContainer.Remove(KeyPrefix + key);
		}

		public void RemoveChildContainer(string tag)
		{
			if (tag == null) { throw new ArgumentNullException(nameof(tag)); }
			
			ISettingsContainer child;
			if (Children.TryGetValue(tag, out child)) {
				child.Clear();
				Children.Remove(tag);

				if (child is IDisposable) {
					((IDisposable)child).Dispose();
				}
			}
		}

		public void Set<T>(string key, T value)
		{
			if (key == null) { throw new ArgumentNullException(nameof(key)); }
			ParentContainer.Set(KeyPrefix + key, value);
		}

		public void SetCryptedString(string key, string value)
		{
			if (key == null) { throw new ArgumentNullException(nameof(key)); }
			ParentContainer.SetCryptedString(KeyPrefix + key, value);
		}

		#region IDisposable メンバ
		bool isDisposed_ = false;
		virtual protected void Dispose(bool disposing)
		{
			if (isDisposed_) { return; }
			if (disposing) {
				ParentContainer.SettingChanging -= ParentContainer_SettingChanging;
				ParentContainer.SettingChanged -= ParentContainer_SettingChanged;
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
