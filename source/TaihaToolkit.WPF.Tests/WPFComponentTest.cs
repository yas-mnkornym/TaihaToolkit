using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Studiotaiha.Toolkit.WPF.Tests
{
	[TestClass]
	public class WPFComponentTest
	{
		[ClassInitialize]
		public static void Initialize(TestContext context)
		{
			TaihaToolkit.Current.RegisterComponent(WPFComponent.Instance);
		}
	}
}
