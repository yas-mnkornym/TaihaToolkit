using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Studiotaiha.Toolkit.Settings.Containers;

namespace Studiotaiha.Toolkit.Core.Tests.Settings.Containers
{
	[TestClass]
	public class ProxySettingsContainerTest
	{
		[TestMethod]
		public void TagTest()
		{
			var parentTag = Guid.NewGuid().ToString();
			var childTag = Guid.NewGuid().ToString();
			var parent = new SettingsContainer(parentTag);

			var child = new ProxySettingsContainer(parent, childTag);
			var po = new PrivateObject(child);

			var expectedPrefix = string.Format("{0}__{1}__\\", parentTag, childTag);
			var actualPrefix = (string)po.GetProperty("KeyPrefix");
			Assert.AreEqual(expectedPrefix, actualPrefix);
		}

		[TestMethod]
		public void TagEscapeTest()
		{
			var tag = "!\"#$%&'()=~|-^\\_?><}*+]:;l/.,";
			var tagEscaped = "!\"#$%&'()=~|-^-_?><}*+]:;l/.,";

			var parentTag = Guid.NewGuid().ToString();
			var childTag = Guid.NewGuid().ToString();
			var parent = new SettingsContainer(tag);

			var child = new ProxySettingsContainer(parent, tag);
			var po = new PrivateObject(child);

			var expectedPrefix = string.Format("{0}__{1}__\\", tagEscaped, tagEscaped);
			var actualPrefix = (string)po.GetProperty("KeyPrefix");
			Assert.AreEqual(expectedPrefix, actualPrefix);
		}
	}
}
