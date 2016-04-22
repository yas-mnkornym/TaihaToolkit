using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Studiotaiha.Toolkit.Settings.Containers;

namespace Studiotaiha.Toolkit.Core.Tests.Settings.Containers
{
	[TestClass]
	public class SettingsContainerTest
	{
		[TestMethod]
		public void SettingsContainerBasicStuffTest()
		{
			var tag = Guid.NewGuid().ToString();
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

			var container = new SettingsContainer(tag);
			Assert.AreEqual(tag, container.Tag);

			int changingCalledCount = 0, changedCalledCount = 0;
			container.SettingChanging += (_, __) => changingCalledCount++;
			container.SettingChanged += (_, __) => changedCalledCount++;

			foreach (var data in testData) {
				container.Set(data.Key, data.Value);
				var actual = container.Get<object>(data.Key);

				Assert.AreEqual(data.Value, actual, data.Key);
			}

			Assert.AreEqual(testData.Count, container.Settings.Count());
			Assert.AreEqual(testData.Count, changingCalledCount);
			Assert.AreEqual(testData.Count, changedCalledCount);
			

			var expectedCount = container.Settings.Count();
			foreach (var data in testData) {
				container.Remove(data.Key);
				expectedCount--;
				Assert.AreEqual(expectedCount, container.Settings.Count());
			}

			Assert.AreEqual(0, container.Settings.Count());

			foreach (var data in testData) {
				container.Set(data.Key, data.Value);
				var actual = container.Get<object>(data.Key);
			}

			Assert.AreEqual(testData.Count, container.Settings.Count());
			container.Clear();
			Assert.AreEqual(0, container.Settings.Count());
		}
		
		[TestMethod]
		public void ChildTest()
		{
			var parent = new SettingsContainer("root");

			var child = parent.GetOrCreateChildContainer("child");
			Assert.AreEqual("child", child.Tag);

			var tag = Guid.NewGuid().ToString();
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
			
			int changingCalledCount = 0, changedCalledCount = 0;
			child.SettingChanging += (_, __) => changingCalledCount++;
			child.SettingChanged += (_, __) => changedCalledCount++;

			int parentChaningCalledCount = 0, parentChangedCalledCount = 0;
			parent.SettingChanging += (_, __) => parentChaningCalledCount++;
			parent.SettingChanged +=(_, __) => parentChangedCalledCount++;

			foreach (var data in testData) {
				child.Set(data.Key, data.Value);
				var actual = child.Get<object>(data.Key);

				Assert.AreEqual(data.Value, actual, data.Key);
			}

			Assert.AreEqual(testData.Count, child.Settings.Count());
			Assert.AreEqual(testData.Count, parent.Settings.Count());

			Assert.AreEqual(testData.Count, changingCalledCount);
			Assert.AreEqual(testData.Count, changedCalledCount);
			Assert.AreEqual(testData.Count, parentChaningCalledCount);
			Assert.AreEqual(testData.Count, parentChangedCalledCount);

			parent.Set("unko", "chinko");
			Assert.AreEqual(testData.Count + 1, parent.Settings.Count());
			Assert.AreEqual(testData.Count, child.Settings.Count());

			parent.Remove("unko");
			Assert.AreEqual(testData.Count, child.Settings.Count());
			Assert.AreEqual(testData.Count, parent.Settings.Count());

			parent.Clear();
			Assert.AreEqual(testData.Count, child.Settings.Count());
			Assert.AreEqual(testData.Count, parent.Settings.Count());

			foreach (var data in testData) {
				child.Set(data.Key, data.Value);
			}

			parent.Set("unko", "chinko");
			Assert.AreEqual(testData.Count + 1, parent.Settings.Count());
			Assert.AreEqual(testData.Count, child.Settings.Count());

			child.Clear();
			Assert.AreEqual(1, parent.Settings.Count());
			Assert.AreEqual(0, child.Settings.Count());
		}
	}
}
