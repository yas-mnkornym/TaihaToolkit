using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace Studiotaiha.Toolkit
{
	public static class ExceptionExt
	{
		/// <summary>
		/// 例外がアプリケーションの実行に対して致命的なものであるかどうかをテストする。
		/// </summary>
		/// <param name="ex">テストする例外</param>
		/// <returns>例外が致命的なものであればtrue, さもなくばfalse</returns>
		public static bool IsCritical(this Exception ex)
		{
			if (ex == null) { throw new NullReferenceException("ExceptionExt.IsCritical() is called with a null exception."); }

			return (
				ex is AccessViolationException ||
				ex is AppDomainUnloadedException ||
				ex is StackOverflowException ||
				ex is SecurityException ||
				ex is SEHException ||
				ex is ThreadAbortException
				);
		}
	}
}
