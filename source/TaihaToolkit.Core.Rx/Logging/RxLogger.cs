using System;
using System.Reactive.Subjects;
using Studiotaiha.Toolkit.Composition;
using Studiotaiha.Toolkit.Logging;

namespace Studiotaiha.Toolkit.Core.Rx.Logging
{
	[ComponentImplementation(typeof(ILogger), 1000)]
	public class RxLogger : LoggerBase, IDisposable
	{
		Subject<LogData> LogSubject { get; } = new Subject<LogData>();

		public RxLogger(string tag)
			: base(tag)
		{ }

		public RxLogger(string tag, ILogger parent)
			: base(tag, parent)
		{ }

		public override ILogger CreateChild(string tag)
		{
			var logger = new RxLogger(tag, this);
			logger.Logged += (_, e) => {
				LogSubject.OnNext(e.LogData);
				RaiseLoggedEvent(e.LogData);
			};
			return logger;
		}

		protected override void OnLogged(LogData logData)
		{
			LogSubject.OnNext(logData);
		}

		public override IObservable<LogData> LogSource => LogSubject;

		#region IDisposable member
		bool isDisposed_ = false;
		virtual protected void Dispose(bool disposing)
		{
			if (isDisposed_) { return; }
			if (disposing) {
				LogSubject.Dispose();
			}
			isDisposed_ = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}
