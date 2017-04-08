using System;
using System.Reflection;
using Studiotaiha.Toolkit.Composition;

namespace Studiotaiha.Toolkit.Logging
{
    public class LoggingComponent : IComponent
	{
		public static Guid ComponentId { get; } = new Guid("0B21FAD1-667E-475D-963D-7912F41E0C6F");
		#region Singleton

		static LoggingComponent instance_;
		public static LoggingComponent Instance => instance_ = (instance_ = new LoggingComponent());

		#endregion // Singleton

		public Type[] CriticalExceptionTypes { get; } = new Type[] { };

		public Assembly Assembly => GetType().GetTypeInfo().Assembly;
		public Guid Id => ComponentId;
	}
}
