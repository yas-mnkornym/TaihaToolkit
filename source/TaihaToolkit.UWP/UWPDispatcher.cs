using System;
using Windows.UI.Core;

namespace Studiotaiha.Toolkit
{
    class UWPDispatcher : IDispatcher
	{
		public CoreDispatcher Dispatcher { get; }

		public UWPDispatcher(CoreDispatcher dispatcher)
		{
			if (dispatcher == null) { throw new ArgumentNullException("dispatcher"); }
			Dispatcher = dispatcher;
		}

		public void Dispatch(Action act)
		{
			Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => act()).AsTask().Wait();
		}

		public T Dispatch<T>(Func<T> func)
		{
			T result = default(T);
			Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
				result = func();
			}).AsTask().Wait();
			return result;
		}

		public void BeginDispatch(
			Action act,
			Action onCompleted = null,
			Action onAborted = null)
		{
			Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => act()).AsTask()
				.ContinueWith(t => {
					onCompleted();
				});
		}

		public void BeginDispatch<T>(
			Func<T> func,
			Action<T> onCompleted = null,
			Action onAborted = null)
		{
			T result = default(T);
            Dispatcher
                .RunAsync(CoreDispatcherPriority.Normal, () => {
                    result = func();
                })
                .AsTask()
                .ContinueWith(t => {
                    onCompleted?.Invoke(result);
                })
                .Wait();
		}
	}
}
