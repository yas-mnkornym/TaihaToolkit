using System;

namespace Studiotaiha.Toolkit.Composition
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class ComponentImplementationAttribute : Attribute
	{
		public ComponentImplementationAttribute(
			Type interfaceType,
			int priority = 10000)
		{
			InterfaceType = interfaceType;
			Priority = priority;
		}

		public Type InterfaceType { get; }
		public int Priority { get; }
	}
}
