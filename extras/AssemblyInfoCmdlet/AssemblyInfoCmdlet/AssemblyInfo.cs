using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyInfoCmdlet
{
	class AssemblyInfo
	{
		[AssemblyInfoProperty]
		public string AssemblyTitle { get; set; }

		[AssemblyInfoProperty]
		public string AssemblyDescription { get; set; }

		[AssemblyInfoProperty]
		public string AssemblyConfiguration { get; set; }

		[AssemblyInfoProperty]
		public string AssemblyCompany { get; set; }

		[AssemblyInfoProperty]
		public string AssemblyProduct { get; set; }

		[AssemblyInfoProperty]
		public string AssemblyCopyright { get; set; }

		[AssemblyInfoProperty]
		public string AssemblyTrademark { get;set;}

		[AssemblyInfoProperty]
		public string AssemblyCulture { get; set; }

		[AssemblyInfoProperty]
		public bool ComVisible { get; set; }

		[AssemblyInfoProperty]
		public Guid Guid { get; set; }

		[AssemblyInfoProperty]
		public string AssemblyVersion { get; set; }

		[AssemblyInfoProperty]
		public string AssemblyFileVersion { get; set; }
	}
}
