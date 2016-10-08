using System;
using System.Collections.Generic;

namespace Studiotaiha.Toolkit.Dialog.LocalizedStringProviders
{
    public class ResourceBagLocalizedStringProviderBase : IDialogLocalizedStringProvider
    {
        IDictionary<string, string> ResourceBag { get; }

        protected ResourceBagLocalizedStringProviderBase(IDictionary<string, string> resourceBag)
        {
            if (resourceBag == null) { throw new ArgumentNullException(nameof(resourceBag)); }
            ResourceBag = resourceBag;
        }

        public string GetString(string resourceName)
        {
            string result = null;
            ResourceBag.TryGetValue(resourceName, out result);
            return result;
        }
    }
}
