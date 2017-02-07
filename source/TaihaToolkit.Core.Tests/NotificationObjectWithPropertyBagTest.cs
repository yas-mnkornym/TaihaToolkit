using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Studiotaiha.Toolkit.Core.Tests
{
	[TestClass]
	public class NotificationObjectWithPropertyBagTest
	{
		[TestMethod]
		public void SetAndGetTest()
		{
			var dataSet = new dynamic[]{
				new DataSet<bool> {
					Assign = true,
					DefaultValue = true,
					SetValue = false,
				},
				new DataSet<bool> {
					Assign = false,
					DefaultValue = true,
					SetValue = true,
				},
				new DataSet<bool>{
					Assign = true,
					DefaultValue = false,
					SetValue = false,
				},

				// string - assign
				new DataSet<string> {
					Assign = true,
					DefaultValue = null,
					SetValue = Guid.NewGuid().ToString(),
				},
				new DataSet<string> {
					Assign = true,
					DefaultValue = Guid.NewGuid().ToString(),
					SetValue = Guid.NewGuid().ToString(),
				},
				new DataSet<string> {
					Assign = true,
					DefaultValue = Guid.NewGuid().ToString(),
					SetValue = null,
				},

				// string - no assign
				new DataSet<string> {
					Assign = false,
					DefaultValue = null,
					SetValue = Guid.NewGuid().ToString(),
				},
				new DataSet<string> {
					Assign = false,
					DefaultValue = Guid.NewGuid().ToString(),
					SetValue = Guid.NewGuid().ToString(),
				},
				new DataSet<string> {
					Assign = false,
					DefaultValue = Guid.NewGuid().ToString(),
					SetValue = null,
				},

				// int - assign
				new DataSet<int> {
					Assign = true,
					DefaultValue = 0,
					SetValue = 1,
				},
				new DataSet<int> {
					Assign = true,
					DefaultValue = 1,
					SetValue = 1,
				},
				new DataSet<int> {
					Assign = true,
					DefaultValue = 1,
					SetValue = 0,
				},

				// int no assign
				new DataSet<int> {
					Assign = false,
					DefaultValue = 0,
					SetValue = 1,
				},
				new DataSet<int> {
					Assign = false,
					DefaultValue = 1,
					SetValue = 1,
				},
				new DataSet<int> {
					Assign = false,
					DefaultValue = 1,
					SetValue = 0,
				},
			};

			var notificationObject = new NotificationObjectWithPropertyBag();
			var po = new PrivateObject(notificationObject);

			int i = 0;
			foreach (var set in dataSet) {
				var propertyName = Guid.NewGuid().ToString();

				if (set.Assign) {
					po.Invoke(
						"SetValue",
						new Type[] {
							set.Type,
							set.ActionType,
							set.ActionType,
							typeof(string),
						},
						new object[] {
							set.SetValue,
							set.ActionBeforeChange,
							set.ActionAfterChagne,
							propertyName,
						},
						new Type[] {
							set.Type
						});
				}

				var actual = po.Invoke(
					"GetValue",
					new Type[] {
						set.Type,
						typeof(string),
					},
					new object[] {
						set.DefaultValue,
						propertyName,
					},
					new Type[] {
						set.Type,
					});

				var expected = set.Assign ? set.SetValue : set.DefaultValue;
				var title = string.Format("{0} - {1}", i++, set.ToString());
				Assert.AreEqual(expected, actual, title);
				if (set.Assign) {
					Assert.IsTrue(set.BeforeCalled, title);
					Assert.IsTrue(set.AfterCalled, title);
				}
			}
		}
		
		class DataSet<T> 
		{
			public DataSet()
			{
				ActionBeforeChange = (oldValue, newValue) => {
					BeforeCalled = true;
					Assert.AreEqual(default(T), oldValue);
					Assert.AreEqual(SetValue, newValue);
				};

				ActionAfterChagne = (oldValue, newValue) => {
					AfterCalled = true;
					Assert.AreEqual(default(T), oldValue);
					Assert.AreEqual(SetValue, newValue);
				};

			}

			public T SetValue { get; set; }
			public T DefaultValue { get; set; }
			public bool Assign { get; set; }

			public Type Type { get; } = typeof(T);
			public Type ActionType { get; } = typeof(Action<T, T>);
			public Action<T, T> ActionBeforeChange { get; }
			public Action<T, T> ActionAfterChagne { get; }

			public bool BeforeCalled { get; private set; }
			public bool AfterCalled { get; private set; }

			public override string ToString()
			{
				return string.Format(
					   "Type: {0} Assign: {1}, Default: {2}, Set: {3}",
					   typeof(T).Name,
					   Assign,
					   DefaultValue,
					   SetValue);
			}
		}
	}
}
