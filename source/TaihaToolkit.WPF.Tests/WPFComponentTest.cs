using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Studiotaiha.Toolkit.WPF.Tests
{
	[TestClass]
	public class WPFComponentTest
	{
		[ClassInitialize]
		public static void Initialize(TestContext context)
		{
			TaihaToolkit.RegisterComponent(WPFComponent.Instance);
		}
	}
}
