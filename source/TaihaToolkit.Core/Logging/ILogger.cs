using System;
using System.Runtime.CompilerServices;

namespace Studiotaiha.Toolkit.Logging
{
	/// <summary>
	/// Represents a logger.
	/// </summary>
	public interface ILogger
	{
		/// <summary>
		/// Gets the logger tag.
		/// </summary>
		string Tag { get; }

		/// <summary>
		/// Log an event.
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="level">Level</param>
		/// <param name="exception">Exception to be associated to the event</param>
		/// <param name="file">Filename</param>
		/// <param name="line">Line number</param>
		/// <param name="member">Member name</param>
		void Log(
			string message,
			ELogLevel level = ELogLevel.Information,
			Exception exception = null,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null
			);
		
		/// <summary>
		/// Create a child logger.
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		ILogger CreateChild(string tag);

		/// <summary>
		/// Gets the parent logger.
		/// </summary>
		ILogger Parent { get; }

		/// <summary>
		/// Gets the root logger.
		/// </summary>
		ILogger Root { get; }

		/// <summary>
		/// Notifies when an event is logged.
		/// </summary>
		/// <remarks>
		/// Logged event of children loggers are redirected to this.
		/// </remarks>
		event EventHandler<LogEventArgs> Logged;

	}
}
