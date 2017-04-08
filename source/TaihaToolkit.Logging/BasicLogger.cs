using Studiotaiha.Toolkit.Composition;

namespace Studiotaiha.Toolkit.Logging
{
	[ComponentImplementation(typeof(ILogger), int.MaxValue)]
	public class BasicLogger : LoggerBase
	{
		public BasicLogger(string tag)
			: base(tag)
		{ }

		public BasicLogger(string tag, ILogger parent)
			: base(tag, parent)
		{ }

		public override ILogger CreateChild(string tag)
		{
			var logger = new BasicLogger(tag, this);
			logger.Logged += (_, e) => {
				RaiseLoggedEvent(e.LogData);
			};
			return logger;
		}
	}
}
