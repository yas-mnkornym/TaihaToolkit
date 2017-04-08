using System;
using System.Reactive.Linq;

namespace Studiotaiha.Toolkit.Logging
{
	public static class LoggerExtensions
	{
		public static IObservable<LogEvent> LoggedAsObesrvable(this ILogger logger)
		{
			return Observable.FromEvent<EventHandler<LogEventArgs>, LogEvent>(
				h => (s, e) => h(e.LogData),
				h => logger.Logged += h,
				h => logger.Logged -=h);
		}
	}
}
