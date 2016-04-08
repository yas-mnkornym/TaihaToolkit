using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Studiotaiha.Toolkit.WPF.Settings
{
	public class SettingsImpl : ISettings
	{
		#region Private Field
		Dictionary<string, object> SettingsDataInternal = new Dictionary<string, object>(); // 設定データ
		Dictionary<string, SettingsImpl> SettingsChildren { get; } = new Dictionary<string, SettingsImpl>(); // 子設定
		ISettingsSerializer ChildSettingsSerializer = new DataContractSettingsSerializer(); // 子設定のシリアライザ 
		#endregion

		#region Properties
		internal Dictionary<string, object> SettingsData { get { return SettingsDataInternal; } }

		protected SettingsImpl ParentSettings { get; private set; }
		#endregion

		#region Constructors
		public SettingsImpl(string tag)
		{
			Tag = tag;
		}

		public SettingsImpl(IEnumerable<Type> knownTypes)
			: this(null, knownTypes)
		{ }

		public SettingsImpl(string tag, IEnumerable<Type> knownTypes)
			: this(tag)
		{
			KnownTypes = knownTypes.ToList();
		}

		protected SettingsImpl(SettingsImpl parentSettings, string tag, IEnumerable<Type> knownTypes)
			: this(tag, knownTypes)
		{
			if (parentSettings == null) { throw new ArgumentNullException("parentSettings"); }
			ParentSettings = parentSettings;

			// タグ編集
			List<string> tags = new List<string>();
			var settings = this;
			while (settings != null) {
				tags.Add(settings.Tag);
				settings = settings.ParentSettings;
			}
			tags.Reverse();
			Tag = string.Join("_", tags);
		}

		#endregion

		#region ISettings メンバ
		public IList<Type> KnownTypes { get; } = new List<Type>();

		public string Tag { get; }

		public void Set<TValue>(string key, TValue value)
		{
			var actualKey = key;
			bool isNewValue = true;
			object oldValue = null;

			if (SettingsData.TryGetValue(actualKey, out oldValue)) {
				if (oldValue != null) {
					isNewValue = (!oldValue.Equals(value));
				}
				else {
					isNewValue = (value != null);
				}
			}
			else {
				isNewValue = true;
			}

			if (isNewValue) {
				var args = new SettingChangeEventArgs(key, oldValue, value);
				SettingChanging?.Invoke(this, args);
				SettingsData[actualKey] = value;
				SettingChanged?.Invoke(this, args);
			}
		}

		public TValue Get<TValue>(string key, TValue defaultValue = default(TValue))
		{
			var actualKey = key;
			object value;
			if (SettingsData.TryGetValue(actualKey, out value)) {
				if (value is TValue) {
					return (TValue)value;
				}
			}
			return defaultValue;
		}

		public void SetCrypted<TValue>(string key, TValue value)
		{
			throw new NotImplementedException();
		}

		public TValue GetDecrypted<TValue>(string key, TValue defaultValue = default(TValue))
		{
			throw new NotImplementedException();
		}

		public bool Exists(string key)
		{
			var actKey = key;
			return SettingsData.ContainsKey(actKey);
		}

		public void Remove(string key)
		{
			var actualKey = key; // GetTaggedKey(key);
			if (SettingsData.ContainsKey(actualKey)) {
				var args = new SettingChangeEventArgs(key, SettingsData[actualKey], null);
				SettingChanging?.Invoke(this, args);
				SettingsData.Remove(actualKey);
				SettingChanged?.Invoke(this, args);
			}
		}

		public void Clear()
		{
			foreach (var key in SettingsData.Keys.ToArray()) {
				Remove(key);
			}
		}

		public IEnumerable<string> Keys => SettingsData.Keys;

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:オブジェクトを複数回破棄しない")]
		public ISettings GetChildSettings(string tag, IEnumerable<Type> knownTypes)
		{
			if (SettingsChildren.ContainsKey(tag)) {
				return SettingsChildren[tag];
			}
			else {
				var settings = new SettingsImpl(this, tag, knownTypes);

				// 設定をロード
				var setTag = GetTaggedKey(settings.Tag, true);
				var setStr = Get<string>(setTag, null);
				if (setStr != null) {
					using (var ms = new MemoryStream())
					using (var writer = new StreamWriter(ms, Encoding.UTF8, 2048, true)) {
						writer.Write(setStr);
						writer.Flush();
						ms.Seek(0, SeekOrigin.Begin);
						ChildSettingsSerializer.Deserialize(ms, settings);
					}
				}

				// 子設定に追加
				settings.SettingChanged += settings_SettingChanged;
				SettingsChildren[settings.Tag] = settings;
				return settings;
			}
		}

		/// <summary>
		/// 子設定が変更されたとき、その値を保存
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void settings_SettingChanged(object sender, SettingChangeEventArgs e)
		{
			var settings = sender as SettingsImpl;
			if (settings != null) {
				var tag = GetTaggedKey(settings.Tag, true);
				string str = null;
				using (var ms = new MemoryStream()) {
					ChildSettingsSerializer.Serialize(ms, settings);
					ms.Seek(0, SeekOrigin.Begin);
					using (var reader = new StreamReader(ms)) {
						str = reader.ReadToEnd();
					}
				}

				// 設定保存
				Set(tag, str);
			}
		}

		public void RemoveChildSettings(string tag)
		{
			if (SettingsChildren.ContainsKey(tag)) {
				var settings = SettingsChildren[tag];
				SettingsChildren[tag] = null;
				settings.SettingChanged -= settings_SettingChanged;
			}
		}

		public IEnumerable<string> ChildSettingsTags => SettingsChildren.Keys;

		public event EventHandler<SettingChangeEventArgs> SettingChanging;

		public event EventHandler<SettingChangeEventArgs> SettingChanged;
		#endregion // ISettings メンバ

		#region Private Methods
		string GetTaggedKey(string key, bool isEmbed = false)
		{
			string result;
			if (Tag == null) {
				result = key;
			}
			else {
				result = string.Format("{0}.{1}", Tag, key);
			}

			if (isEmbed) {
				return "__" + result;
			}
			else {
				return result;
			}
		}
		#endregion
	}
}
