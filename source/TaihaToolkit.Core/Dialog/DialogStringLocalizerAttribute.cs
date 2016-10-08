using System;
using System.Linq;

namespace Studiotaiha.Toolkit.Dialog
{
	[System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	sealed class DialogStringLocalizerAttribute : Attribute
	{
		public DialogStringLocalizerAttribute(params string[] supportedCultures)
		{
			SupportedCultures = supportedCultures
				.Select(x => x?.ToLower())
				.ToArray();
		}

		public string[] SupportedCultures { get; }
	}
}
