using System;

namespace AssemblyInfoCmdlet
{
	[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	public sealed class AssemblyInfoPropertyAttribute : Attribute
	{
		public string PropertyName { get; set; }
	}
}
