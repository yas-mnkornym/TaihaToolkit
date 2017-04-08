using Studiotaiha.Toolkit.Composition;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TaihaToolkit.Core.Tests")]

namespace Studiotaiha.Toolkit.Settings
{
	public class SettingsService
	{
		public static Guid ServiceIdSettings { get; } = new Guid("D8C079FF-C37B-461F-BF3E-8280733F58E6");

		#region Singleton

		static SettingsService instance_;
		public static SettingsService Instance => instance_ ?? (instance_ = new SettingsService());

		#endregion // Singleton

		private SettingsService()
		{
			TaihaToolkit.Current.RegisterComponent(SettingsComponent.Instance);
		}

		public ISettingsContainer CreateContainer(string tag)
		{
			var loader = new ComponentImplementationLoader<ISettingsContainer>();
			return loader.CreateInstance(tag);
		}

		public ISettingsSerializer CreateDefaultSerializer()
		{
			var loader = new ComponentImplementationLoader<ISettingsSerializer>();
			return loader.CreateInstance();
		}
	}
}
