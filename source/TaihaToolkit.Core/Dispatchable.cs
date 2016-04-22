using System;

namespace Studiotaiha.Toolkit
{
	public class Dispatchable
	{
		/// <summary>
		/// Gets or sets the dispatcher to be used to dispatch actions.
		/// </summary>
		public IDispatcher Dispatcher { get; protected set; }

		/// <summary>
		/// Constructor
		/// </summary>
		public Dispatchable()
		{ }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="dispatcher">Dispatcher to be associated</param>
		public Dispatchable(IDispatcher dispatcher)
		{
			Dispatcher = dispatcher;
		}

		/// <summary>
		/// Dispatch an action to the UI thread.
		/// </summary>
		/// <param name="act">Action to be dispatched.</param>
		/// <remarks>If this method is called from UI thread, the action is executed directly.</remarks>
		protected void Dispatch(Action act)
		{
			if (act == null) { throw new ArgumentNullException(nameof(act)); }

			if (Dispatcher != null) {
				Dispatcher.Dispatch(act);
			}
			else {
				act();
			}
		}

		/// <summary>
		/// Dispatch a func to the UI thread.
		/// </summary>
		/// <typeparam name="T">Type of the return value of the func</typeparam>
		/// <param name="func">Func to be dispatched.</param>
		/// <remarks>If this method is called from UI thread, the func is executed directly.</remarks>
		/// <returns>Return value of the func</returns>
		protected T Dispatch<T>(Func<T> func)
		{
			if (func == null) { throw new ArgumentNullException(nameof(func)); }

			if (Dispatcher != null) {
				return Dispatcher.Dispatch(func);
			}
			else {
				return func();
			}
		}

		/// <summary>
		/// Post an action to dispatch to the UI thread.
		/// </summary>
		/// <param name="act">Action to be dispatched.</param>
		/// <param name="onCompleted">An action invoked when the action successfully invoked.</param>
		/// <param name="onAborted">An action invoked when the action is aborted.</param>
		/// <remarks>If this method is called from UI thread, the action is executed directly.</remarks>
		protected void BeginDispatch(
			Action act,
			Action onCompleted = null,
			Action onAborted = null)
		{
			if (act == null) { throw new ArgumentNullException(nameof(act)); }

			if (Dispatcher != null) {
				Dispatcher.BeginDispatch(act, onCompleted, onAborted);
			}
			else {
				act();
			}
		}

		/// <summary>
		/// Post an func to dispatch to the UI thread.
		/// </summary>
		/// <typeparam name="T">Type of the return value of the func</typeparam>
		/// <param name="func">Func to be dispatched.</param>
		/// <param name="onCompleted">An action invoked when the func is successfully invoked.</param>
		/// <param name="onAborted">An action invoked when the func is aborted.</param>
		void BeginDispatch<T>(
			Func<T> func,
			Action<T> onCompleted = null,
			Action onAborted = null)
		{
			if (func == null) { throw new ArgumentNullException(nameof(func)); }

			if (Dispatcher != null) {
				Dispatcher.BeginDispatch(func, onCompleted, onAborted);
			}
			else {
				var ret = func();
				onCompleted(ret);
			}
		}
	}
}
