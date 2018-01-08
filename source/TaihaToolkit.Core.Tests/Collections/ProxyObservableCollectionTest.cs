using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Studiotaiha.Toolkit.Collections;

namespace Studiotaiha.Toolkit.Core.Tests.Collections
{
	[TestClass]
	public class ProxyObservableCollectionTest
	{
		[TestMethod]
		public void TestAddAndReset()
		{
			var original = new ObservableCollection<int>();
			original.Add(1);
			original.Add(2);

			var proxy = new ProxyObservableCollection<int, Container>(
				original,
				value => new Container {
					Value = value,
				});

			Assert.IsTrue(Enumerable.SequenceEqual(
				original,
				proxy.Select(x => x.Value)));

			for (int i = 0; i < 10; i++) {
				original.Add(i);
			}

			Assert.AreEqual(12, original.Count);
			Assert.AreEqual(12, proxy.Count);
			Assert.IsTrue(Enumerable.SequenceEqual(
				original,
				proxy.Select(x => x.Value)));

			original.Clear();
			Assert.AreEqual(0, original.Count);
			Assert.AreEqual(0, proxy.Count);
		}

		[TestMethod]
		public void TestAddAndResetReverse()
		{
			var original = new ObservableCollection<int>();
			original.Add(1);
			original.Add(2);

			var proxy = new ProxyObservableCollection<int, Container>(
				original,
				value => new Container {
					Value = value,
				},
				item => item.Value);

			Assert.IsTrue(Enumerable.SequenceEqual(
				original,
				proxy.Select(x => x.Value)));

			for (int i = 0; i < 10; i++) {
				proxy.Add(new Container {
					Value = i,
				});
			}

			Assert.AreEqual(12, original.Count);
			Assert.AreEqual(12, proxy.Count);
			Assert.IsTrue(Enumerable.SequenceEqual(
				original,
				proxy.Select(x => x.Value)));

			original.Clear();
			Assert.AreEqual(0, original.Count);
			Assert.AreEqual(0, proxy.Count);
		}

		[TestMethod]
		public void TestRemove()
		{
			var original = new ObservableCollection<int>(Enumerable.Range(0, 10));
			var proxy = new ProxyObservableCollection<int, Container>(
				original,
				value => new Container {
					Value = value,
				});

			for (int i = 0; i < 5; i++) {
				original.RemoveAt(i);
			}

			Assert.AreEqual(5, original.Count);
			Assert.AreEqual(5, proxy.Count);
			Assert.IsTrue(Enumerable.SequenceEqual(
				original,
				proxy.Select(x => x.Value)));
		}

		[TestMethod]
		public void TestRemoveReverse()
		{
			var original = new ObservableCollection<int>(Enumerable.Range(0, 10));
			var proxy = new ProxyObservableCollection<int, Container>(
				original,
				value => new Container {
					Value = value,
				});

			for (int i = 0; i < 5; i++) {
				proxy.RemoveAt(i);
			}

			Assert.AreEqual(5, original.Count);
			Assert.AreEqual(5, proxy.Count);
			Assert.IsTrue(Enumerable.SequenceEqual(
				original,
				proxy.Select(x => x.Value)));
		}
		
		[TestMethod]
		public void TestMove()
		{
			var original = new ObservableCollection<int>(Enumerable.Range(0, 10));
			var proxy = new ProxyObservableCollection<int, Container>(
				original,
				value => new Container {
					Value = value,
				});

			for (int i = 0; i < 4; i++) {
				original.Move(i, i * 2);
			}

			Assert.AreEqual(10, original.Count);
			Assert.AreEqual(10, proxy.Count);
			Assert.IsTrue(Enumerable.SequenceEqual(
				original,
				proxy.Select(x => x.Value)));
		}
		
		[TestMethod]
		public void TestMoveReverse()
		{
			var original = new ObservableCollection<int>(Enumerable.Range(0, 10));
			var proxy = new ProxyObservableCollection<int, Container>(
				original,
				value => new Container {
					Value = value,
				},
				item => item.Value);

			for (int i = 0; i < 4; i++) {
				proxy.Move(i, i * 2);
			}

			Assert.AreEqual(10, original.Count);
			Assert.AreEqual(10, proxy.Count);
			Assert.IsTrue(Enumerable.SequenceEqual(
				original,
				proxy.Select(x => x.Value)));
		}

		[TestMethod]
		public void TestReplace()
		{
			var original = new ObservableCollection<int>(Enumerable.Range(0, 10));
			var proxy = new ProxyObservableCollection<int, Container>(
				original,
				value => new Container {
					Value = value,
				});

			for (int i = 0; i < 4; i++) {
				original[i] = i * 20;
			}

			Assert.AreEqual(10, original.Count);
			Assert.AreEqual(10, proxy.Count);
			Assert.IsTrue(Enumerable.SequenceEqual(
				original,
				proxy.Select(x => x.Value)));
		}

		[TestMethod]
		public void TestReplaceReverses()
		{
			var original = new ObservableCollection<int>(Enumerable.Range(0, 10));
			var proxy = new ProxyObservableCollection<int, Container>(
				original,
				value => new Container {
					Value = value,
				},
				item => item.Value);

			for (int i = 0; i < 4; i++) {
				proxy[i] = new Container {
					Value = i * 20,
				};
			}

			Assert.AreEqual(10, original.Count);
			Assert.AreEqual(10, proxy.Count);
			Assert.IsTrue(Enumerable.SequenceEqual(
				original,
				proxy.Select(x => x.Value)));
		}

		[TestMethod]
		public void TestReverseFailure()
		{
			var original = new ObservableCollection<int>(Enumerable.Range(0, 10));
			var proxy = new ProxyObservableCollection<int, Container>(
				original,
				value => new Container {
					Value = value,
				});

			try {
				proxy.Move(0, 1);
				Assert.Fail();
			}
			catch (InvalidOperationException) { }

			try {
				proxy[0] = new Container {
					Value = 1,
				};
				Assert.Fail();
			}
			catch (InvalidOperationException) { }
		}


		[TestMethod]
		public void TestDispatchNone()
		{
			var dispatcher = new DispatcherStub();
			var original = new ObservableCollection<int>(Enumerable.Range(0, 10));
			var proxy = new ProxyObservableCollection<int, Container>(
				original,
				value => new Container {
					Value = value,
				},
				item => item.Value,
				dispatcher,
				ProxyDispatchMode.None);

			original.Add(0);
			Assert.IsFalse(dispatcher.DispatchCalled);

			proxy.Add(new Container { Value = 0 });
			Assert.IsFalse(dispatcher.DispatchCalled);
		}
		
		[TestMethod]
		public void TestDispatchOneWay()
		{
			var dispatcher = new DispatcherStub();
			var original = new ObservableCollection<int>(Enumerable.Range(0, 10));
			var proxy = new ProxyObservableCollection<int, Container>(
				original,
				value => new Container {
					Value = value,
				},
				item => item.Value,
				dispatcher,
				ProxyDispatchMode.OneWay);

			original.Add(0);
			Assert.IsTrue(dispatcher.DispatchCalled);

			dispatcher.DispatchCalled = false;
			proxy.Add(new Container { Value = 0 });
			Assert.IsFalse(dispatcher.DispatchCalled);
		}

		[TestMethod]
		public void TestDispatchOneWayToSource()
		{
			var dispatcher = new DispatcherStub();
			var original = new ObservableCollection<int>(Enumerable.Range(0, 10));
			var proxy = new ProxyObservableCollection<int, Container>(
				original,
				value => new Container {
					Value = value,
				},
				item => item.Value,
				dispatcher,
				ProxyDispatchMode.OneWayToSource);

			original.Add(0);
			Assert.IsFalse(dispatcher.DispatchCalled);
			
			proxy.Add(new Container { Value = 0 });
			Assert.IsTrue(dispatcher.DispatchCalled);
		}

		[TestMethod]
		public void TestDispatchTwoWay()
		{
			var dispatcher = new DispatcherStub();
			var original = new ObservableCollection<int>(Enumerable.Range(0, 10));
			var proxy = new ProxyObservableCollection<int, Container>(
				original,
				value => new Container {
					Value = value,
				},
				item => item.Value,
				dispatcher,
				ProxyDispatchMode.TwoWay);

			original.Add(0);
			Assert.IsTrue(dispatcher.DispatchCalled);

			dispatcher.DispatchCalled = false;
			proxy.Add(new Container { Value = 0 });
			Assert.IsTrue(dispatcher.DispatchCalled);
		}

		class DispatcherStub : IDispatcher
		{
			public bool DispatchCalled { get; set; }

			public void BeginDispatch(Action act, Action onCompleted = null, Action onAborted = null)
			{
				throw new NotImplementedException();
			}

			public void BeginDispatch<T>(Func<T> func, Action<T> onCompleted = null, Action onAborted = null)
			{
				throw new NotImplementedException();
			}

			public void Dispatch(Action act)
			{
				DispatchCalled = true;
				act.Invoke();
			}

			public T Dispatch<T>(Func<T> func)
			{
				return func.Invoke();
			}
		}

		public class Container
		{
			public int Value { get; set; }
		}
	}
}
