using System.Collections.Generic;

namespace Studiotaiha.Toolkit.Dialog.LocalizedStringProviders
{
    [DialogLocalizedStringProvider("en", "en-us")]
	public class EnglishLocalizedStringProvider : ResourceBagLocalizedStringProviderBase
    {
        public EnglishLocalizedStringProvider()
            : base(new Dictionary<string, string> {
                [DialogLocalizerStringResourceNames.Error] = "Error",
                [DialogLocalizerStringResourceNames.Warning] = "Warning",
                [DialogLocalizerStringResourceNames.Information] = "Information",
                [DialogLocalizerStringResourceNames.Debug] = "Debug",
                [DialogLocalizerStringResourceNames.Question] = "Question",
                [DialogLocalizerStringResourceNames.Yes] = "Yes",
                [DialogLocalizerStringResourceNames.No] = "No",
                [DialogLocalizerStringResourceNames.Ok] = "OK",
                [DialogLocalizerStringResourceNames.Cancel] = "Cancel",
                [DialogLocalizerStringResourceNames.Select] = "Select",
                [DialogLocalizerStringResourceNames.Confirmation] = "Confirmation",
                [DialogLocalizerStringResourceNames.Default] = "Default",
            })
        {
        }
    }
}