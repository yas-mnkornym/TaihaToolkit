using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Studiotaiha.Toolkit.WPF.Tests
{
	[TestClass]
	public class ExceptionExtTest
	{
		[ClassInitialize]
		public static void Initialize(TestContext context)
		{
			TaihaToolkit.Current.RegisterComponent(WPFComponent.Instance);
		}

		[TestMethod]
		public void ExceptionExtWithWpfComponentTest()
		{
			var expectedExceptions = CoreComponent.Instance.CriticalExceptionTypes
				.Concat(WPFComponent.Instance.CriticalExceptionTypes)
				.Distinct();
			var actualExceptions = ExceptionExt.CriticalExceptionTypes;

			Assert.IsTrue(Enumerable.SequenceEqual(
				expectedExceptions.OrderBy(x => x.GUID),
				actualExceptions.OrderBy(x => x.GUID)
			));
		}
	}
}
