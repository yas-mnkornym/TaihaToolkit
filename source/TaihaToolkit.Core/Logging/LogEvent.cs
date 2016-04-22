using System;

namespace Studiotaiha.Toolkit.Logging
{
	/// <summary>
	/// Log event
	/// </summary>
	public class LogEvent
	{
		/// <summary>
		/// Gets the logger tag which logged this event.
		/// </summary>
		public string Tag { get; set; }

		/// <summary>
		/// Gets parent logger tags.
		/// </summary>
		public string[] ParentTags { get; set; }

		/// <summary>
		/// Gets the message.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Gets the log level.
		/// </summary>
		public ELogLevel Level { get; set; }

		/// <summary>
		/// Gets the exception object associated to the event.
		/// </summary>
		public Exception Exception { get; set; }

		/// <summary>
		/// Gets the file name which logged this event.
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// Gets the line number where this event is logged.
		/// </summary>
		public int LineNumber { get; set; }

		/// <summary>
		/// Gets the member name which logged this event.
		/// </summary>
		public string MemberName { get; set; }
	}
}
