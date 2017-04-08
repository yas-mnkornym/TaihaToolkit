using System;
using System.Reflection;
using Studiotaiha.Toolkit.Composition;

namespace Studiotaiha.Toolkit.Settings
{
	public class SettingsComponent : IComponent
	{
		public static Guid ComponentId { get; } = new Guid("08A8CBB0-CBCE-4FEE-9F6D-439E9B3AB1B6");

		#region Singleton

		static SettingsComponent instance_;
		public static SettingsComponent Instance => instance_ = (instance_ = new SettingsComponent());

		#endregion // Singleton

		public Type[] CriticalExceptionTypes { get; } = new Type[] { };

		public Assembly Assembly => GetType().GetTypeInfo().Assembly;
		public Guid Id => ComponentId;
	}
}
