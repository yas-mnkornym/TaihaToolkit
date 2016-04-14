using System;
using System.Collections.Generic;
using System.Linq;
using AssemblyInfoCmdlet;
using AssemblyInfoCmdletTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssemblyInfoCmdletTest
{
	[TestClass]
	public class AssemblyInfoParserTest
	{
		[TestMethod]
		public void ParseTest()
		{
			var parser = new AssemblyInfoParser();
			var actualProperties = parser.ReadProperties(InputText);
			Assert.IsNotNull(actualProperties);
			Assert.AreEqual(12, actualProperties.Length);

			var expectedProperties = new Dictionary<string, PropertyValue[]> {
				["AssemblyTitle"] = new PropertyValue[] { new PropertyValue { Type = PropertyValueType.String, Value = "AssemblyInfoCmdlet" } },
				["AssemblyDescription"] = new PropertyValue[] { new PropertyValue { Type = PropertyValueType.String, Value = "" } },
				["AssemblyConfiguration"] = new PropertyValue[] { new PropertyValue { Type = PropertyValueType.String, Value = "" } },
				["AssemblyCompany"] = new PropertyValue[] { new PropertyValue { Type = PropertyValueType.String, Value = "" } },
				["AssemblyProduct"] = new PropertyValue[] { new PropertyValue { Type = PropertyValueType.String, Value = "AssemblyInfoCmdlet" } },
				["AssemblyCopyright"] = new PropertyValue[] { new PropertyValue { Type = PropertyValueType.String, Value = "Copyright ©  2016" } },
				["AssemblyTrademark"] = new PropertyValue[] { new PropertyValue { Type = PropertyValueType.String, Value = "" } },
				["AssemblyCulture"] = new PropertyValue[] { new PropertyValue { Type = PropertyValueType.String, Value = "" } },
				["ComVisible"] = new PropertyValue[] { new PropertyValue { Type = PropertyValueType.Unknown, Value = "false" } },
				["Guid"] = new PropertyValue[] { new PropertyValue { Type = PropertyValueType.String, Value = "601ad923-baa2-4eef-9f24-bc655a24bd6c" } },
				["AssemblyVersion"] = new PropertyValue[] { new PropertyValue { Type = PropertyValueType.String, Value = "1.0.0.0" } },
				["AssemblyFileVersion"] = new PropertyValue[] { new PropertyValue { Type = PropertyValueType.String, Value = "1.0.0.0" } },
			};
			Assert.IsTrue(Enumerable.SequenceEqual(
				expectedProperties.Keys.OrderBy(x => x),
				actualProperties.Select(x => x.Name).OrderBy(x => x)));
			foreach (var property in actualProperties) {
				var expectedValues = expectedProperties[property.Name];
				var actualValues = property.Values;
				Assert.IsNotNull(expectedValues);
				Assert.IsNotNull(actualValues);
				Assert.AreEqual(expectedValues.Length, actualValues.Length);

				Assert.IsTrue(Enumerable.SequenceEqual(
					expectedValues.Select(x => x.Type),
					actualValues.Select(x => x.Type)));

				Assert.IsTrue(Enumerable.SequenceEqual(
					expectedValues.Select(x => x.Value),
					actualValues.Select(x => x.Value)),
					string.Format("Expected: {0}, Actual: {1}",
						string.Join(", ", expectedValues.Select(x => x.Value)),
						string.Join(", ", actualValues.Select(x => x.Value)))
				);
			}

		}
		
		[TestMethod]
		public void DeserializeTest()
		{
			var parser = new AssemblyInfoParser();
			var properties = parser.ReadProperties(InputText);
			var deserializer = new AssemblyInfoDeserializer<AssemblyInfo>();
			var info = deserializer.Deserialize(properties);
			Assert.AreEqual("AssemblyInfoCmdlet", info.AssemblyTitle);
			Assert.AreEqual("", info.AssemblyDescription);
			Assert.AreEqual("", info.AssemblyConfiguration);
			Assert.AreEqual("", info.AssemblyCompany);
			Assert.AreEqual("AssemblyInfoCmdlet", info.AssemblyProduct);
			Assert.AreEqual("Copyright ©  2016", info.AssemblyCopyright);
			Assert.AreEqual("", info.AssemblyTrademark);
			Assert.AreEqual("", info.AssemblyCulture);
			Assert.AreEqual(false, info.ComVisible);
			Assert.AreEqual(Guid.Parse("601ad923-baa2-4eef-9f24-bc655a24bd6c"), info.Guid);
			Assert.AreEqual("1.0.0.0", info.AssemblyVersion);
			Assert.AreEqual("1.0.0.0", info.AssemblyFileVersion);
		}

	static string InputText { get; } = @"
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// アセンブリに関する一般情報は以下の属性セットをとおして制御されます。
// アセンブリに関連付けられている情報を変更するには、
// これらの属性値を変更してください。
[assembly: AssemblyTitle(""AssemblyInfoCmdlet"")]
[assembly: AssemblyDescription("""")]
[assembly: AssemblyConfiguration("""")]
[assembly: AssemblyCompany("""")]
[assembly: AssemblyProduct(""AssemblyInfoCmdlet"")]
[assembly: AssemblyCopyright(""Copyright ©  2016"")]
[assembly: AssemblyTrademark("""")]
[assembly: AssemblyCulture("""")]

// ComVisible を false に設定すると、その型はこのアセンブリ内で COM コンポーネントから 
// 参照不可能になります。COM からこのアセンブリ内の型にアクセスする場合は、
// その型の ComVisible 属性を true に設定してください。
[assembly: ComVisible(false)]

// このプロジェクトが COM に公開される場合、次の GUID が typelib の ID になります
[assembly: Guid(""601ad923-baa2-4eef-9f24-bc655a24bd6c"")]

// アセンブリのバージョン情報は次の 4 つの値で構成されています:
//
//      メジャー バージョン
//      マイナー バージョン
//      ビルド番号
//      Revision
//
// すべての値を指定するか、下のように '*' を使ってビルドおよびリビジョン番号を 
// 既定値にすることができます:
// [assembly: AssemblyVersion(""1.0.*"")]
[assembly: AssemblyVersion(""1.0.0.0"")]
[assembly: AssemblyFileVersion(""1.0.0.0"")]
";
	}
}
