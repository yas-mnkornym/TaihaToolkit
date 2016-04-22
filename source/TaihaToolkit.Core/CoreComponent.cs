using System;
using System.Reflection;
using System.Security;
using Studiotaiha.Toolkit.Composition;

namespace Studiotaiha.Toolkit
{
	public class CoreComponent : IComponent
	{
		public static Guid ComponentId { get; } = new Guid("143873BB-3709-4DF6-B558-910B6746CFEF");

		#region Singleton
		static CoreComponent instance_;
		public static CoreComponent Instance => instance_ ?? (instance_ = new CoreComponent());
		#endregion // Singleton

		private CoreComponent()
		{ }

		public Assembly Assembly => GetType().GetTypeInfo().Assembly;

		public Guid Id => ComponentId;

		public Type[] CriticalExceptionTypes { get; } = new Type[] {
			typeof(SecurityException),
		};
	}
}
