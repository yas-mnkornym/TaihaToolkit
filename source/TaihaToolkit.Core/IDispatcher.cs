using System;

namespace Studiotaiha.Toolkit
{
	public interface IDispatcher
	{
		void Dispatch(Action act);

		T Dispatch<T>(Func<T> func);

		void BeginDispatch(
			Action act,
			Action onCompleted = null,
			Action onAborted = null);

		void BeginDispatch<T>(
			Func<T> func,
			Action<T> onCompleted = null,
			Action onAborted = null);
	}
}
