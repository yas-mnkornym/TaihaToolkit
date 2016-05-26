using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Studiotaiha.Toolkit.Core.Tests
{
	[TestClass]
	public class NotificationObjectWithPropertyBagTest
	{
		[TestMethod]
		public void SetValueTest()
		{
			var notificationObject = new NotificationObjectWithPropertyBag();
			var po = new PrivateObject(notificationObject);

			var propertyName = Guid.NewGuid().ToString();
			var expectedNewValue = Guid.NewGuid().ToString();
			var beforeCalled = false;
			var afterCalled = false;

			Action<string, string> actionBeforeChange = (oldValue, newValue) => {
				beforeCalled = true;
				Assert.AreEqual(null, oldValue);
				Assert.AreEqual(expectedNewValue, newValue);
			};

			Action<string, string> actionAfterChange = (oldValue, newValue) => {
				afterCalled = true;
				Assert.AreEqual(null, oldValue);
				Assert.AreEqual(expectedNewValue, newValue);
			};

			po.Invoke(
				"SetValue",
				new Type[] {
					typeof(string),
					typeof(Action<string, string>),
					typeof(Action<string, string>),
					typeof(string),
				},
				new object[] {
					expectedNewValue,
					actionBeforeChange,
					actionAfterChange,
					propertyName,
				},
				new Type[] {
					typeof(string)
				});

			var actualNewValue = po.Invoke(
				"GetValue",
				new Type[] {
					typeof(string),
					typeof(string),
				},
				new object[] {
					null,
					propertyName,
				},
				new Type[] {
					typeof(string)
				});

			Assert.AreEqual(expectedNewValue, actualNewValue);
			Assert.IsTrue(beforeCalled);
			Assert.IsTrue(afterCalled);
		}
	}
}
