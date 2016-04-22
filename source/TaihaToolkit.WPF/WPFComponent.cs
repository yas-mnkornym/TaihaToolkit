using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using Studiotaiha.Toolkit.Composition;

namespace Studiotaiha.Toolkit
{
	public sealed class WPFComponent : IComponent
	{
		#region Singleton

		static WPFComponent instance_;
		public static WPFComponent Instance => instance_ ?? (instance_ = new WPFComponent());

		private WPFComponent()
		{ }

		#endregion // Singleton

		public static Guid ComponentId { get; } = new Guid("7A27BE45-872B-4825-B268-09D7B310CE19");

		public Assembly Assembly => this.GetType().Assembly;

		public Type[] CriticalExceptionTypes { get; } = new Type[] {
			typeof(AccessViolationException),
			typeof(AppDomainUnloadedException),
			typeof(StackOverflowException),
			typeof(SecurityException),
			typeof(SEHException),
			typeof(ThreadAbortException),
		};

		public Guid Id => ComponentId;
	}
}
