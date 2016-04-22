using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Studiotaiha.Toolkit.Settings.Containers;
using Studiotaiha.Toolkit.Settings.Serializers;

namespace Studiotaiha.Toolkit.Core.Tests.Settings.Serializers
{
	[TestClass]
	public class DataContractXmlSettingsSerializerTest
	{
		[TestMethod]
		public void SerializeAndDeserializeSettingsTest()
		{
			var container = new SettingsContainer("root");
			container.Set("minusInt", -1);
			container.Set("zeroInt", 0);
			container.Set("plusInto", 1);
			container.Set("minusDouble", -1.5);
			container.Set("zeroDouble", 0.0);
			container.Set("plusDouble", 1.5);
			container.Set("emptystring", "");
			container.Set("string", "unko");
			container.Set("DateTime", DateTime.Now);
			container.Set("DateTiemOffest", DateTimeOffset.Now);
			container.Set("Guid", Guid.NewGuid());
			container.Set("Class", new SettingsTestClass {
				Test = new SettingsTestClass {
					Int = 10,
					String = Guid.NewGuid().ToString(),
					Float = 10.5f,
				},
				Int = 1,
				String = Guid.NewGuid().ToString(),
				Float = 5.5f,
			});
			container.Set<object>("null", null);

			var serializer = new DataContractXmlSettingsSerializer();
			var deserializedContainer = new SettingsContainer("root");
			using (var ms = new MemoryStream()) {
				serializer.Serialize(ms, container);

				ms.Seek(0, SeekOrigin.Begin);
				serializer.Deserialize(ms, deserializedContainer);
			}

			CollectionAssert.AreEqual(
				new List<string>(container.Keys),
				new List<string>(deserializedContainer.Keys));

			CollectionAssert.AreEqual(
				new List<object>(container.Settings.Select(x => x.Value)),
				new List<object>(deserializedContainer.Settings.Select(x => x.Value)));
		}

		[TestMethod]
		public void SerializeAndDeserializeKeyValuePairTest()
		{
			var testData = new Dictionary<string, object> {
				["minusInt"] = -1,
				["zeroInt"] = 0,
				["plusInto"] = 1,
				["minusDouble"] = -1.5,
				["zeroDouble"] = 0.0,
				["plusDouble"] = 1.5,
				["emptystring"] = "",
				["string"] = "unko",
				["DateTime"] = DateTime.Now,
				["DateTiemOffest"] = DateTimeOffset.Now,
				["Guid"] = Guid.NewGuid(),
				["Class"] = new SettingsTestClass {
					Test = new SettingsTestClass {
						Int = 10,
						String = Guid.NewGuid().ToString(),
						Float = 10.5f
					},
					Int = 1,
					String = Guid.NewGuid().ToString(),
					Float = 5.5f,
				},
				["null"] = null,
			};

			var serializer = new PrivateObject(new DataContractXmlSettingsSerializer());

			foreach (var data in testData) {
				var ret = serializer.Invoke("SerializeKeyValuePair", data);
				var deserialized = (KeyValuePair<string, object>)serializer.Invoke("DeserializeKeyValuePair", ret);

				Assert.AreEqual(data.Key, deserialized.Key, data.Key);
				Assert.AreEqual(data.Value, deserialized.Value, data.Key);
			}
		}
	}
}
