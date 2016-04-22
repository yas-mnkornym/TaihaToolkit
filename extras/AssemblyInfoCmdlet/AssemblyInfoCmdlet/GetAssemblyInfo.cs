using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyInfoCmdlet
{
	[Cmdlet(VerbsCommon.Get, "AssemblyInfo")]
	public class GetAssemblyInfo : PSCmdlet
	{
		protected override void ProcessRecord()
		{		
			var currentLocation = this.SessionState.Path.CurrentLocation.Path;
			var inputFile = Path.IsPathRooted(FilePath) ? FilePath : Path.Combine(currentLocation, FilePath);

			var parser = new AssemblyInfoParser();
			var text = System.IO.File.ReadAllText(inputFile);
			var properties = parser.ReadProperties(text);
			var deserializer = new AssemblyInfoDeserializer<AssemblyInfo>();
			var info = deserializer.Deserialize(properties);
			WriteObject(info);
		}

		[Parameter(Position = 0)]
		public string FilePath { get; set; }
	}
}
