using Studiotaiha.Toolkit.Composition;

namespace Studiotaiha.Toolkit.Settings
{
	public class SettingsService
	{
		#region Singleton

		static SettingsService instance_;
		public static SettingsService Instance => instance_ ?? (instance_ = new SettingsService());

		#endregion // Singleton

		private SettingsService()
		{ }

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
