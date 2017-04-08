using System;
using System.Reflection;
using Studiotaiha.Toolkit.Composition;

namespace Studiotaiha.Toolkit.Dialog
{
    public class DialogComponent : IComponent
	{
		public static Guid ComponentId { get; } = new Guid("3F14670F-CCE4-420A-A4DA-290D04427EB7");
		#region Singleton

		static DialogComponent instance_;
		public static DialogComponent Instance => instance_ = (instance_ = new DialogComponent());

		#endregion // Singleton

		public Type[] CriticalExceptionTypes { get; } = new Type[] { };

		public Assembly Assembly => GetType().GetTypeInfo().Assembly;
		public Guid Id => ComponentId;
	}
}
