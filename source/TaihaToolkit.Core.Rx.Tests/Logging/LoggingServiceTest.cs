using Microsoft.VisualStudio.TestTools.UnitTesting;
using Studiotaiha.Toolkit.Core.Rx.Logging;

namespace Studiotaiha.Toolkit.Core.Rx.Tests.Logging
{
	[TestClass]
	public class LoggingServiceTest
	{
		[TestMethod]
		public void LoggerCreationTest()
		{
			TaihaToolkit.Initialize(RxComponent.Instance);
			var logger = LoggingService.Current.RootLogger;
			Assert.IsNotNull(logger);
			Assert.IsTrue(logger is RxLogger);
		}
	}
}
