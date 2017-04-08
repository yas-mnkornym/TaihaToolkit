using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Studiotaiha.Toolkit.Settings;

namespace Studiotaiha.Toolkit.Core.Rx.Tests.Settings
{
	[TestClass]
	public class SettingsExtensionsTest
	{
		[ClassInitialize]
		public static void ClassInitialize(TestContext testContext)
		{
		}

		[TestMethod]
		public void SettingsSubscriptionTest()
		{
			var settings = Enumerable.Range(1, 1000)
				.Select(x => new KeyValuePair<string, object>(Guid.NewGuid().ToString(), Guid.NewGuid()))
				.ToArray();

			var changingQueue = new Queue<KeyValuePair<string, object>>(settings);
			var changedQueue = new Queue<KeyValuePair<string, object>>(settings);

			var container = SettingsService.Instance.CreateContainer("unko");
			Assert.IsNotNull(container);

			var subscriptions = new CompositeDisposable();

			subscriptions.Add(container.SettingChangingAsObservable().Subscribe(e => {
				var next = changingQueue.Dequeue();
				Assert.AreEqual(next.Key, e.Key);
				Assert.AreEqual(next.Value, e.NewValue);
			}));

			subscriptions.Add(container.SettingChangedAsObservable().Subscribe(e => {
				var next = changedQueue.Dequeue();
				Assert.AreEqual(next.Key, e.Key);
				Assert.AreEqual(next.Value, e.NewValue);
			}));

			using (subscriptions) {
				foreach (var setting in settings) {
					container.Set(setting.Key, setting.Value);
				}
			}

			Assert.AreEqual(0, changingQueue.Count);
			Assert.AreEqual(0, changedQueue.Count);
		}
	}
}
