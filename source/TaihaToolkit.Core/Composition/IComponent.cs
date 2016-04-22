using System;
using System.Reflection;

namespace Studiotaiha.Toolkit.Composition
{
	public interface IComponent
	{
		/// <summary>
		/// Gets the ID of the component.
		/// </summary>
		Guid Id { get; }

		/// <summary>
		/// Gets the assembly object of the component.
		/// </summary>
		Assembly Assembly { get; }

		Type[] CriticalExceptionTypes { get; }
	}
}
