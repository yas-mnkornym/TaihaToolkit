using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Studiotaiha.Toolkit.Composition;

namespace Studiotaiha.Toolkit
{
	public class LoggingService
	{
		public static string RootLoggerTag { get; } = "root";

		#region シングルトン実装
		static LoggingService current_ = null;
		public static LoggingService Current
		{
			get
			{
				return current_ ?? (current_ = new LoggingService());
			}
		}
		#endregion // シングルトン実装

		public ILogger RootLogger { get; private set; }

		private LoggingService()
		{
			RootLogger = CreateLogger(RootLoggerTag);
		}

		ILogger CreateLogger(string tag)
		{
			var loader = new ComponentImplementationLoader<ILogger>();
			return loader.CreateInstance(tag);
		}

		public ILogger GetLogger(string tag)
		{
			if (RootLogger == null) { throw new InvalidOperationException("LoggerService is not initialized"); }
			if (tag == null) { throw new ArgumentNullException("tag"); }
			return RootLogger.CreateChild(tag);
		}

		public ILogger GetLogger(object owner)
		{
			if (RootLogger == null) { throw new InvalidOperationException("LoggerService is not initialized"); }
			if (owner == null) { throw new ArgumentNullException("owner"); }
			return GetLogger(owner.GetType());
		}

		public ILogger GetLogger(Type type)
		{
			if (RootLogger == null) { throw new InvalidOperationException("LoggerService is not initialized"); }
			if (type == null) { throw new ArgumentNullException("type"); }
			return GetLogger(type.Name);
		}
	}
}
