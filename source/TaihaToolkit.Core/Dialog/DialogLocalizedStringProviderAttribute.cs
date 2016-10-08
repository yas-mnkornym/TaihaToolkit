using System;
using System.Linq;

namespace Studiotaiha.Toolkit.Dialog
{
	[System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	sealed class DialogLocalizedStringProviderAttribute : Attribute
	{
		public DialogLocalizedStringProviderAttribute(params string[] supportedCultures)
		{
			SupportedCultures = supportedCultures
				.Select(x => x?.ToLower())
				.ToArray();
		}

		public string[] SupportedCultures { get; }
	}
}
