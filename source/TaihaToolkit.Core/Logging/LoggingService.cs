using System;
using Studiotaiha.Toolkit.Composition;

namespace Studiotaiha.Toolkit.Logging
{
	public class LoggingService
	{
		public static string RootLoggerTag { get; } = "root";

		#region Singleton

		static LoggingService current_ = null;
		public static LoggingService Current => current_ ?? (current_ = new LoggingService());

		#endregion

		public ILogger RootLogger { get; private set; }

		private LoggingService()
		{
			RootLogger = CreateLogger(RootLoggerTag);
			if (RootLogger == null) { throw new InvalidOperationException("Failed to create root logger."); }
		}

		ILogger CreateLogger(string tag)
		{
			var loader = new ComponentImplementationLoader<ILogger>();
			return loader.CreateInstance(tag);
		}

		public ILogger GetLogger(string tag)
		{
			if (tag == null) { throw new ArgumentNullException("tag"); }
			return RootLogger.CreateChild(tag);
		}

		public ILogger GetLogger(object owner)
		{
			if (owner == null) { throw new ArgumentNullException("owner"); }
			return GetLogger(owner.GetType());
		}

		public ILogger GetLogger(Type type)
		{
			if (type == null) { throw new ArgumentNullException("type"); }
			return GetLogger(type.Name);
		}
	}
}
