using System;
using System.Reactive.Linq;

namespace Studiotaiha.Toolkit.Settings
{
	public static class SettingsContainerExtensions
	{
		public static IObservable<SettingChangeEventArgs> SettingChangingAsObservable(this ISettingsContainer container)
		{
			return Observable.FromEvent<EventHandler<SettingChangeEventArgs>, SettingChangeEventArgs>(
				h => (s, e) => h(e),
				h => container.SettingChanging += h,
				h => container.SettingChanging -= h);
		}

		public static IObservable<SettingChangeEventArgs> SettingChangedAsObservable(this ISettingsContainer container)
		{
			return Observable.FromEvent<EventHandler<SettingChangeEventArgs>, SettingChangeEventArgs>(
				h => (s, e) => h(e),
				h => container.SettingChanged += h,
				h => container.SettingChanged -= h);
		}
	}
}
