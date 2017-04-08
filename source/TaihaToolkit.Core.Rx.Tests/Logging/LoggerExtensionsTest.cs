using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Studiotaiha.Toolkit.Logging;

namespace Studiotaiha.Toolkit.Core.Rx.Tests.Logging
{
	[TestClass]
	public class LoggerExtensionsTest
	{
		[ClassInitialize]
		public static void ClassInitialize(TestContext testContext)
		{
		}
		
		[TestMethod]
		public void LoggerSubscriptionTest()
		{
			var logStrings = Enumerable.Range(1, 1000)
				.Select(x => Guid.NewGuid().ToString())
				.ToArray();
			var queue = new Queue<string>(logStrings);

			var logger = LoggingService.Current.GetLogger(this);
			Assert.IsNotNull(logger);

			var subscription = logger.LoggedAsObesrvable().Subscribe(x => {
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
