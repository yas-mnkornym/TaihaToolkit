using System;
using System.Collections.Generic;
using System.Globalization;
using Studiotaiha.Toolkit.Composition;
using Studiotaiha.Toolkit.Dialog.LocalizedStringProviders;

namespace Studiotaiha.Toolkit.Dialog
{
    public sealed class DialogService
	{
		#region Singleton

		static DialogService instance_;
		public static DialogService Instance => instance_ ?? (instance_ = new DialogService());

		#endregion

		/// <summary>
		/// Gets current dialog manager
		/// </summary>
        IDialogManager dialogManager_;
		public IDialogManager DialogManager
        {
            get
            {
                if (dialogManager_ == null) {
                    RecreateManager();
                }

                return dialogManager_;
            }
        }

		private DialogService()
		{
			RegisterLocalizerGenerator(CultureInfo.InvariantCulture, () => new EnglishLocalizedStringProvider());
			RegisterLocalizerGenerator(new CultureInfo("ja-jp"), () => new JapaneseLocalizedStringProvider());
			RegisterLocalizerGenerator(new CultureInfo("en-us"), () => new EnglishLocalizedStringProvider());
		}

		public void RecreateManager()
		{			
			var loader = new ComponentImplementationLoader<IDialogManager>();
			dialogManager_ = loader.CreateInstance();
			
			DialogManagerChanged?.Invoke(this, DialogManager);
		}

		Dictionary<CultureInfo, Func<IDialogLocalizedStringProvider>> CultureLocalizerGeneratorMap { get; } = new Dictionary<CultureInfo, Func<IDialogLocalizedStringProvider>>();
		public void RegisterLocalizerGenerator(CultureInfo culture, Func<IDialogLocalizedStringProvider> generator)
		{
			if (generator == null) { throw new ArgumentNullException(nameof(generator)); }
			CultureLocalizerGeneratorMap[culture] = generator;
		}

		public IDialogLocalizedStringProvider GetLocalizedStringProvider(CultureInfo culture)
		{
            Func<IDialogLocalizedStringProvider> generator;
            CultureLocalizerGeneratorMap.TryGetValue(culture, out generator);
            return generator?.Invoke();
		}

		public event EventHandler<IDialogManager> DialogManagerChanged;
	}
}
