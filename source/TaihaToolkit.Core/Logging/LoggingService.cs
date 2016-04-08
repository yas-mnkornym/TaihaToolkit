using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Toolkit
{
	public class LoggingService
	{
		#region シングルトン実装
		static LoggingService current_ = null;
		public static LoggingService Current
		{
			get
			{
				return current_ ?? (current_ = new LoggingService(new Logger("root")));
			}
		}
		#endregion // シングルトン実装

		public ILogger RootLogger{get; private set;}

		private LoggingService(ILogger rootLogger)
		{
			RootLogger = rootLogger;
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
