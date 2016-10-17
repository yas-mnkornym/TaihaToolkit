using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Studiotaiha.Toolkit.Utilities.EnumUtilities;

namespace Studiotaiha.Toolkit.Core.Tests.Utilities.EnumUtilities
{
	[TestClass]
	public class EnumExtensionsTest
	{
		[TestMethod]
		public void SplidFlagsTest()
		{
			var expected = new TestEnum[] {
				TestEnum.Value1,
				TestEnum.Value2,
				TestEnum.Value3
			};

			var ret = EnumExtensions.SplitFlags<TestEnum>(TestEnum.CombinedValue, true);
			Assert.IsTrue(Enumerable.SequenceEqual(expected, ret));
		}

		[Flags]
		enum TestEnum
		{
			DefaultValue = 0,
			Value1 = 1,
			Value2 = 1 << 1,
			Value3 = 1 << 2,

			[CombinedFlag]
			CombinedValue = Value1 | Value2 | Value3,
		}
	}
}
