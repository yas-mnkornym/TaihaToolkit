using System;
using System.Runtime.CompilerServices;

namespace Studiotaiha.Toolkit.Logging
{
	public static class LoggerExtentions
	{
		#region Error
		public static void Error(
			this ILogger logger,
			string message,
			Exception ex = null,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null)
		{
			logger.Log(message, ELogLevel.Error, ex, file, line, member);
		}

		public static void ErrorFormat(
			this ILogger logger,
			Exception ex,
			string message,
			object[] args,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null)
		{
			logger.Error(string.Format(message, args), ex, file, line, member);
		}
		public static void ErrorFormat(
			this ILogger logger,
			string message,
			object[] args,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null)
		{
			logger.ErrorFormat((Exception)null, message, args, file, line, member);
		}
		#endregion

		#region Warning
		public static void Warning(
			this ILogger logger,
			string message,
			Exception ex = null,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null)
		{
			logger.Log(message, ELogLevel.Warning, ex, file, line, member);
		}

		public static void WarningFormat(
			this ILogger logger,
			Exception ex,
			string message,
			object[] args,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null)
		{
			logger.Warning(string.Format(message, args), ex, file, line, member);
		}
		public static void WarningFormat(
			this ILogger logger,
			string message,
			object[] args,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null)
		{
			logger.WarningFormat((Exception)null, message, args, file, line, member);
		}
		#endregion

		#region Fatal
		public static void Fatal(
			this ILogger logger,
			string message,
			Exception ex = null,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null)
		{
			logger.Log(message, ELogLevel.Fatal, ex, file, line, member);
		}

		public static void FatalFormat(
			this ILogger logger,
			Exception ex,
			string message,
			object[] args,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null)
		{
			logger.Fatal(string.Format(message, args), ex, file, line, member);
		}
		public static void FatalFormat(
			this ILogger logger,
			string message,
			object[] args,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null)
		{
			logger.FatalFormat((Exception)null, message, args, file, line, member);
		}
		#endregion

		#region Information
		public static void Information(
			this ILogger logger,
			string message,
			Exception ex = null,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null)
		{
			logger.Log(message, ELogLevel.Information, ex, file, line, member);
		}

		public static void InformationFormat(
			this ILogger logger,
			Exception ex,
			string message,
			object[] args,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null)
		{
			logger.Information(string.Format(message, args), ex, file, line, member);
		}
		public static void InformationFormat(
			this ILogger logger,
			string message,
			object[] args,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null)
		{
			logger.InformationFormat((Exception)null, message, args, file, line, member);
		}
		#endregion

		#region Verbose
		public static void Verbose(
			this ILogger logger,
			string message,
			Exception ex = null,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null)
		{
			logger.Log(message, ELogLevel.Verbose, ex, file, line, member);
		}

		public static void VerboseFormat(
			this ILogger logger,
			Exception ex,
			string message,
			object[] args,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null)
		{
			logger.Verbose(string.Format(message, args), ex, file, line, member);
		}
		public static void VerboseFormat(
			this ILogger logger,
			string message,
			object[] args,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null)
		{
			logger.VerboseFormat((Exception)null, message, args, file, line, member);
		}
		#endregion


		#region Debug
		public static void Debug(
			this ILogger logger,
			string message,
			Exception ex = null,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null)
		{
			logger.Log(message, ELogLevel.Debug, ex, file, line, member);
		}

		public static void DebugFormat(
			this ILogger logger,
			Exception ex,
			string message,
			object[] args,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null)
		{
			logger.Debug(string.Format(message, args), ex, file, line, member);
		}
		public static void DebugFormat(
			this ILogger logger,
			string message,
			object[] args,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null)
		{
			logger.DebugFormat((Exception)null, message, args, file, line, member);
		}
		#endregion
	}
}
