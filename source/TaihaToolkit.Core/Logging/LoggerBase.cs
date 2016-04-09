using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Toolkit.Logging
{
	public abstract class LoggerBase : ILogger
	{
		protected string[] ParentTags { get; }
		ILogger root_;


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="tag">The logger tag</param>
		public LoggerBase(string tag)
		{
			if (tag == null) { throw new ArgumentNullException("tag"); }
			Tag = tag;
		}


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="tag">The logger tag</param>
		/// <param name="parent">Parent logger</param>
		public LoggerBase(
			string tag,
			ILogger parent)
			: this(tag)
		{
			if (parent == null) { throw new ArgumentNullException("parent"); }
			Parent = parent;
			
			// Retrieve parent tags recursively.
			var parentLogger = parent;
			var parentTags = new List<string>();
			do {
				parentTags.Add(parentLogger.Tag);
				parentLogger = parentLogger.Parent;
			}
			while (parentLogger != null);

			// Reorder parent tags
			parentTags.Reverse();
			ParentTags = parentTags.ToArray();
		}

		public virtual IObservable<LogData> LogSource
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public ILogger Parent { get; }

		public ILogger Root
		{
			get
			{
				if (root_ == null) {
					var logger = Parent;
					while(logger?.Parent != null) {
						logger = logger.Parent;
					}
					root_ = logger;
				}
				return root_;
			}
		}

		public string Tag { get; }

		public abstract ILogger CreateChild(string tag);

		public virtual void Log(string message, ELogLevel level = ELogLevel.Information, Exception exception = null, [CallerFilePath] string file = null, [CallerLineNumber] int line = 0, [CallerMemberName] string member = null)
		{
			var data = new LogData {
				Message = message,
				Level = level,
				Exception = exception,
				FileName = file,
				LineNumber = line,
				MemberName = member,
				Tag = Tag,
				ParentTags = ParentTags
			};

			// Rxで通知
			OnLogged(data);

			// イベントで通知
			RaiseLoggedEvent(data);
		}

		protected virtual void OnLogged(LogData logData) { }

		protected void RaiseLoggedEvent(LogData logData)
		{
			Logged?.Invoke(this, new LogEventArgs(logData));
		}

		public event EventHandler<LogEventArgs> Logged;
	}
}
