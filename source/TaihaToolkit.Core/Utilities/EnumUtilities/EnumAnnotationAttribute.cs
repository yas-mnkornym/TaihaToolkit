using System;

namespace Studiotaiha.Toolkit.Utilities.EnumUtilities
{
	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = true)]
	public sealed class EnumAnnotationAttribute : Attribute
	{
		public EnumAnnotationAttribute(
			string annotationId,
			string value)
		{
			AnnotationId = annotationId;
			Value = value;
		}

		public string AnnotationId { get; }

		public string Value { get; }
	}
}
