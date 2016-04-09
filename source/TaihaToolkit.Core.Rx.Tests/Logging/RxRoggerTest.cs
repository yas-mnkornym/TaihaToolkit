using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Studiotaiha.Toolkit.Core.Rx.Logging;

namespace Studiotaiha.Toolkit.Core.Rx.Tests.Logging
{
	[TestClass]
	public class RxRoggerTest
	{
		[ClassInitialize]
		public static void ClassInitialize(TestContext testContext)
		{
			TaihaToolkit.Initialize(RxComponent.Instance);
		}
		
		[TestMethod]
		public void LoggerSubscriptionsTest()
		{
			var logStrings = Enumerable.Range(1, 1000)
				.Select(x => Guid.NewGuid().ToString())
				.ToArray();
			Queue<string> queue = new Queue<string>(logStrings);

			var logger = LoggingService.Current.GetLogger(this);
			Assert.IsTrue(logger is RxLogger);

			var subscription = logger.LogSource.Subscribe(x => {
				var next = queue.Dequeue();
				Assert.AreEqual(next, x.Message);
			});

			using (subscription) {
				foreach (var str in logStrings) {
					logger.Information(str);
				}
			}

			Assert.AreEqual(0, queue.Count);
		}

	}
}
