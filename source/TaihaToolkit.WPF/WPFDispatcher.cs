using System;
using System.Threading;
using System.Windows.Threading;

namespace Studiotaiha.Toolkit
{
	public class WPFDispatcher : IDispatcher
	{
		public Dispatcher Dispatcher { get; }

		public WPFDispatcher(Dispatcher dispatcher)
		{
			if (dispatcher == null) { throw new ArgumentNullException("dispatcher"); }
			Dispatcher = dispatcher;
		}

		public void Dispatch(Action act)
		{
			if (Thread.CurrentThread.ManagedThreadId != Dispatcher.Thread.ManagedThreadId) {
				Dispatcher.Invoke(act);
			}
			else {
				act();
			}
		}

		public T Dispatch<T>(Func<T> func)
		{
			if (Thread.CurrentThread.ManagedThreadId != Dispatcher.Thread.ManagedThreadId) {
				return Dispatcher.Invoke(func);
			}
			else {
				return func();
			}
		}

		public void BeginDispatch(
			Action act,
			Action onCompleted = null,
			Action onAborted = null)
		{
			var ret = Dispatcher.BeginInvoke(act);
			ret.Completed += (_, __) => {
				onCompleted();
			};
			ret.Aborted += (_, __) => {
				onAborted();
			};
		}

		public void BeginDispatch<T>(
			Func<T> func,
			Action<T> onCompleted = null,
			Action onAborted = null)
		{
			var ret = Dispatcher.BeginInvoke(func);
			ret.Completed += (_, __) => {
				onCompleted((T)ret.Result);
			};
			ret.Aborted += (_, __) => {
				onAborted();
			};
		}
	}
}
