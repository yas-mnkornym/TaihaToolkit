using System;
using System.Reflection;
using Studiotaiha.Toolkit.Composition;

namespace Studiotaiha.Toolkit
{
	public class RxComponent : IComponent
	{
		public static Guid ComponentId { get; } = new Guid("16521540-00E0-4BBE-A5A5-83F8984AE185");

		#region Singleton
		static RxComponent instance_;
		public static RxComponent Instance => instance_ ?? (instance_ = new RxComponent());
		#endregion // Singleton

		private RxComponent()
		{ }

		public Assembly Assembly => GetType().GetTypeInfo().Assembly;

		public Guid Id => ComponentId;

		public Type[] CriticalExceptionTypes { get; } = new Type[] { };
	}
}
