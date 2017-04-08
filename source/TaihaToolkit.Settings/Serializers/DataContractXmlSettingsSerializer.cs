using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Studiotaiha.Toolkit.Composition;
using Studiotaiha.Toolkit.Logging;

namespace Studiotaiha.Toolkit.Settings.Serializers
{
	[ComponentImplementation(typeof(ISettingsSerializer), int.MaxValue)]
	public class DataContractXmlSettingsSerializer : ISettingsSerializer
	{
		ILogger Logger { get; } = LoggingService.Current.GetLogger(typeof(DataContractXmlSettingsSerializer));

		public void Serialize(Stream stream, ISettingsContainer container)
		{
			var serializationInfoArray = container.Settings
				.Select(SerializeKeyValuePair)
				.ToArray();
			var serializer = new DataContractSerializer(typeof(KeyValueSerializationInfo[]));
			serializer.WriteObject(stream, serializationInfoArray);
		}

		public void Deserialize(Stream stream, ISettingsContainer container)
		{
			var serializer = new DataContractSerializer(typeof(KeyValueSerializationInfo[]));
			var serializationInfoArray = (KeyValueSerializationInfo[])serializer.ReadObject(stream);

			container.Clear();

			if (serializationInfoArray != null) {
				var settings = serializationInfoArray.Select(DeserializeKeyValuePair);
				foreach (var setting in settings) {
					container.Set(setting.Key, setting.Value);
				}
			}
		}

		KeyValueSerializationInfo SerializeKeyValuePair(KeyValuePair<string, object> pair)
		{
			var info = new KeyValueSerializationInfo {
				Key = pair.Key,
				TypeName = pair.Value?.GetType()?.AssemblyQualifiedName
			};

			if (pair.Value == null) {
				info.SerializedValue = null;
			}
			else {
				info.SerializedValue = null;
				var serializer = new DataContractSerializer(pair.Value.GetType());

				using (var ms = new MemoryStream()) {
					serializer.WriteObject(ms, pair.Value);

					var buffer = ms.ToArray();
					info.SerializedValue = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
				}
			}

			return info;
		}

		KeyValuePair<string, object> DeserializeKeyValuePair(KeyValueSerializationInfo info)
		{
			var key = info.Key;
			object value = null;

			if (!string.IsNullOrWhiteSpace(info.SerializedValue)) {
				var type = Type.GetType(info.TypeName, false);
				if (type != null) {
					var serializer = new DataContractSerializer(type);
					using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(info.SerializedValue))) {
						value = serializer.ReadObject(ms);
					}
				}
			}

			return new KeyValuePair<string, object>(key, value);
		}

		[DataContract]
		class KeyValueSerializationInfo
		{
			[DataMember]
			public string TypeName { get; set; }

			[DataMember]
			public string Key { get; set; }

			[DataMember(Name = "Value")]
			public string SerializedValue { get; set; }
		}
	}
}
