using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Studiotaiha.Toolkit.Settings;
using Studiotaiha.Toolkit.Settings.Containers;
using Studiotaiha.Toolkit.Settings.Serializers;

namespace Studiotaiha.Toolkit.Core.Tests.Settings
{
	[TestClass]
	public class SettingsServiceTest
	{
		[TestMethod]
		public void CreateContainerTest()
		{
			var tag = Guid.NewGuid().ToString();
			var container = SettingsService.Instance.CreateContainer(tag);

			Assert.IsNotNull(container);
			Assert.IsInstanceOfType(container, typeof(SettingsContainer));
			Assert.AreEqual(tag, container.Tag);
		}

		[TestMethod]
		public void CreateDefaultSerializerTest()
		{
			var serializer = SettingsService.Instance.CreateDefaultSerializer();

			Assert.IsNotNull(serializer);
			Assert.IsInstanceOfType(serializer, typeof(DataContractXmlSettingsSerializer));
		}
	}
}
