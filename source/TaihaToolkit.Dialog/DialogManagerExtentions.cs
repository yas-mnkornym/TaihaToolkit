using System;
using System.Linq;
using System.Threading.Tasks;
using Studiotaiha.Toolkit.Dialog.LocalizedStringProviders;

namespace Studiotaiha.Toolkit.Dialog
{
    public static class DialogManagerExtentions
    {
        public static IDialogLocalizedStringProvider DefaultLocalizedStringProvider { get; } = new EnglishLocalizedStringProvider();
        
		/// <summary>
		/// Sets the current localized string provider.
		/// </summary>
		public static IDialogLocalizedStringProvider CurrentLocalizedStringProvider { get; set; } = DefaultLocalizedStringProvider;

        public static string GetLocalizedString(string resourceName)
        {
            return CurrentLocalizedStringProvider?.GetString(resourceName)
                ?? DefaultLocalizedStringProvider.GetString(resourceName);
        }

		#region ErrorMessage
		public static void ShowErrorMessage(
			this IDialogManager dialogManager,
			string message,
			string caption = null,
			Exception ex = null)
		{
			dialogManager.ShowMessage(message, caption ?? GetLocalizedString(DialogLocalizerStringResourceNames.Ok), ex, EDialogType.Error);
		}

		public static void ShowErrorMessage(
			this IDialogManager dialogManager,
			string message,
			Exception ex)
		{
			dialogManager.ShowErrorMessage(message, null, ex);
		}

		public static Task ShowErrorMessageAsync(
			this IDialogManager dialogManager,
			string message,
			string caption = null,
			Exception ex = null,
			Action onOk = null)
		{
			return dialogManager.ShowMessageAsync(message, caption ?? GetLocalizedString(DialogLocalizerStringResourceNames.Error), ex, EDialogType.Error, (selection) => { if (onOk != null) { onOk(); } });
		}

		public static Task ShowErrorMessageAsync(
			this IDialogManager dialogManager,
			string message,
			Exception ex,
			Action onOk = null)
		{
			return dialogManager.ShowErrorMessageAsync(message, null, ex, onOk);
		}
		#endregion // ErrorMessage

		#region WarningMessage

		public static void ShowWarningMessage(
			this IDialogManager dialogManager,
			string message,
			string caption = null,
			Exception ex = null)
		{
			dialogManager.ShowMessage(message, caption ?? GetLocalizedString(DialogLocalizerStringResourceNames.Warning), ex, EDialogType.Warning);
		}

		public static void ShowWarningMessage(
			this IDialogManager dialogManager,
			string message,
			Exception ex)
		{
			dialogManager.ShowWarningMessage(message, null, ex);
		}

		public static Task ShowWarningMessageAsync(
			this IDialogManager dialogManager,
			string message,
			string caption = null,
			Exception ex = null,
			Action onOk = null)
		{
			return dialogManager.ShowMessageAsync(message, caption ?? GetLocalizedString(DialogLocalizerStringResourceNames.Warning), ex, EDialogType.Warning, (selection) => { if (onOk != null) { onOk(); } });
		}

		public static Task ShowWarningMessageAsync(
			this IDialogManager dialogManager,
			string message,
			Exception ex,
			Action onOk = null)
		{
			return dialogManager.ShowWarningMessageAsync(message, null, ex, onOk);
		}
		#endregion // WarningMessage

		#region InformationMessage

		public static void ShowInformationMessage(
			this IDialogManager dialogManager,
			string message,
			string caption = null,
			Exception ex = null)
		{
			dialogManager.ShowMessage(message, caption ?? GetLocalizedString(DialogLocalizerStringResourceNames.Information), ex, EDialogType.Information);
		}

		public static void ShowInformationMessage(
			this IDialogManager dialogManager,
			string message,
			Exception ex)
		{
			dialogManager.ShowInformationMessage(message, null, ex);
		}

		public static Task ShowInformationMessageAsync(
			this IDialogManager dialogManager,
			string message,
			string caption = null,
			Exception ex = null,
			Action onOk = null)
		{
			return dialogManager.ShowMessageAsync(message, caption ?? GetLocalizedString(DialogLocalizerStringResourceNames.Information), ex, EDialogType.Information, (selection) => { if (onOk != null) { onOk(); } });
		}

		public static Task ShowInformationMessageAsync(
			this IDialogManager dialogManager,
			string message,
			Exception ex,
			Action onOk = null)
		{
			return dialogManager.ShowInformationMessageAsync(message, null, ex, onOk);
		}
		#endregion // InformationMessage

		#region DebugMessage

		public static void ShowDebugMessage(
			this IDialogManager dialogManager,
			string message,
			string caption = null,
			Exception ex = null)
		{
			dialogManager.ShowMessage(message, caption ?? GetLocalizedString(DialogLocalizerStringResourceNames.Debug), ex, EDialogType.Debug);
		}

		public static void ShowDebugMessage(
			this IDialogManager dialogManager,
			string message,
			Exception ex)
		{
			dialogManager.ShowDebugMessage(message, null, ex);
		}

		public static Task ShowDebugMessageAsync(
			this IDialogManager dialogManager,
			string message,
			string caption = null,
			Exception ex = null,
			Action onOk = null)
		{
			return dialogManager.ShowMessageAsync(message, caption ?? GetLocalizedString(DialogLocalizerStringResourceNames.Debug), ex, EDialogType.Debug, (selection) => { if (onOk != null) { onOk(); } });
		}

		public static Task ShowDebugMessageAsync(
			this IDialogManager dialogManager,
			string message,
			Exception ex,
			Action onOk = null)
		{
			return dialogManager.ShowDebugMessageAsync(message, null, ex, onOk);
		}
		#endregion // DebugMessage

		#region Question
		public static bool AskYesNo(
			this IDialogManager dialogManager,
			string message,
			string caption = null,
			Exception ex = null)
		{
			var yes = GetLocalizedString(DialogLocalizerStringResourceNames.Yes);
			var no = GetLocalizedString(DialogLocalizerStringResourceNames.No);
			var ret = dialogManager.ShowMessage(message,
				caption ?? GetLocalizedString(DialogLocalizerStringResourceNames.Confirmation),
				ex, EDialogType.Question,
				yes, no);
			return (ret == yes);
		}

		public static Task<string> AskYesNoAsync(
			this IDialogManager dialogManager,
			string message,
			string caption = null,
			Exception ex = null,
			Action<bool> onSelected = null)
		{
			var yes = GetLocalizedString(DialogLocalizerStringResourceNames.Yes);
			var no = GetLocalizedString(DialogLocalizerStringResourceNames.No);
			return dialogManager.ShowMessageAsync(message,
				caption ?? GetLocalizedString(DialogLocalizerStringResourceNames.Confirmation),
				ex, EDialogType.Question,
				selection => { if (onSelected != null) { onSelected(selection == yes); } },
				yes, no);
		}

		public static bool AskOkCancel(
			this IDialogManager dialogManager,
			string message,
			string caption = null,
			Exception ex = null)
		{
			var ok = GetLocalizedString(DialogLocalizerStringResourceNames.Ok);
			var cancel = GetLocalizedString(DialogLocalizerStringResourceNames.Cancel);
			var ret = dialogManager.ShowMessage(message,
				caption ?? GetLocalizedString(DialogLocalizerStringResourceNames.Confirmation),
				ex, EDialogType.Question,
				ok, cancel);
			return (ret == ok);
		}

		public static Task<string> AskOkCancelAsync(
			this IDialogManager dialogManager,
			string message,
			string caption = null,
			Exception ex = null,
			Action<bool> onSelected = null)
        {
            var ok = GetLocalizedString(DialogLocalizerStringResourceNames.Ok);
            var cancel = GetLocalizedString(DialogLocalizerStringResourceNames.Cancel);
            return dialogManager.ShowMessageAsync(message,
                caption ?? GetLocalizedString(DialogLocalizerStringResourceNames.Confirmation),
                ex, EDialogType.Question,
				selection => { if (onSelected != null) { onSelected(selection == ok); } },
				ok, cancel);
		}

		public static bool? AskYesNoCancel(
			this IDialogManager dialogManager,
			string message,
			string caption = null,
			Exception ex = null)
        {
            var yes = GetLocalizedString(DialogLocalizerStringResourceNames.Yes);
            var no = GetLocalizedString(DialogLocalizerStringResourceNames.No);
            var cancel = GetLocalizedString(DialogLocalizerStringResourceNames.Cancel);
            var ret = dialogManager.ShowMessage(message,
                caption ?? GetLocalizedString(DialogLocalizerStringResourceNames.Confirmation),
                ex, EDialogType.Question,
				yes, no, cancel);
			if (ret == yes) {
				return true;
			}
			else if (ret == no) {
				return false;
			}
			else {
				return null;
			}
		}

		public static Task<string> AskYesNoCancelAsync(
			this IDialogManager dialogManager,
			string message,
			string caption = null,
			Exception ex = null,
			Action<bool?> onSelected = null)
        {
            var yes = GetLocalizedString(DialogLocalizerStringResourceNames.Yes);
            var no = GetLocalizedString(DialogLocalizerStringResourceNames.No);
            var cancel = GetLocalizedString(DialogLocalizerStringResourceNames.Cancel);
            return dialogManager.ShowMessageAsync(message,
                caption ?? GetLocalizedString(DialogLocalizerStringResourceNames.Confirmation),
                ex, EDialogType.Question,
				selection => {
					if (onSelected != null) {
						bool? result = null;
						if (selection == yes) {
							result = true;
						}
						else if (selection == no) {
							result = false;
						}
						onSelected(result);
					}
				},
				yes, no, cancel);
		}

		public static string ShowSelections(
			this IDialogManager dialogManager,
			string message,
			string caption = null,
			Exception ex = null,
			params string[] selections)
		{
			var ret = dialogManager.ShowMessage(
				message,
                caption ?? GetLocalizedString(DialogLocalizerStringResourceNames.Confirmation),
                ex, EDialogType.Question, selections);
			return ret;
		}

		public static string ShowSelections(
			this IDialogManager dialogManager,
			string message,
			string caption = null,
			params string[] selections)
		{
			var ret = dialogManager.ShowMessage(
				message,
                caption ?? GetLocalizedString(DialogLocalizerStringResourceNames.Confirmation),
                null, EDialogType.Question, selections);
			return ret;
		}


		public static Task<string> ShowSelectionsAsync(
			this IDialogManager dialogManager,
			string message,
			string caption = null,
			Exception ex = null,
			Action<string> onSelected = null,
			params string[] selections)
		{
			return dialogManager.ShowMessageAsync(
                message,
                caption ?? GetLocalizedString(DialogLocalizerStringResourceNames.Confirmation),
                ex, EDialogType.Question, onSelected, selections);
		}

		public static Task<string> ShowSelectionsAsync(
			this IDialogManager dialogManager,
			string message,
			string caption = null,
			Action<string> onSelected = null,
			params string[] selections)
		{
			return dialogManager.ShowMessageAsync(
                message,
                caption ?? GetLocalizedString(DialogLocalizerStringResourceNames.Confirmation),
                null, EDialogType.Question, onSelected, selections);
		}
		#endregion

		static DialogSelection OkSelection(int id, bool isDefault) => new DialogSelection {
			Id = id,
			Content = GetLocalizedString(DialogLocalizerStringResourceNames.Ok),
			IsDefault = isDefault
		};

		/// <summary>
		/// Show dialog and wait for it closes.
		/// </summary>
		/// <param name="initializer">Dialog initializer</param>
		/// <param name="dialogType">Alert type</param>
		/// <param name="selections">Selections</param>
		/// <returns></returns>
		public static string ShowDialog(
			this IDialogManager dialogManager,
			Action<IDialogConfig<DialogSelection>> initializer,
			EDialogType dialogType,
			params string[] selections)
		{
			return dialogManager.ShowDialog(
				initializer,
				dialogType,
				selections?.Any() == true
					? selections?.Select((x, i) => new DialogSelection {
						Id = i,
						Content = x,
						IsDefault = (i == 0)
					}).ToArray()
					: new DialogSelection[] { OkSelection(0, true) }
			)
			?.Content as string;
		}

		/// <summary>
		/// Show dialog.
		/// </summary>
		/// <param name="initializer">Dialog initializer</param>
		/// <param name="dialogType">Alert type</param>
		/// <param name="selections">Selections</param>
		/// <returns></returns>
		public static async Task<string> ShowDialogAsync(
			this IDialogManager dialogManager,
			Action<IDialogConfig<DialogSelection>> initializer,
			EDialogType dialogType,
			Action<string> onResult,
			params string[] selections)
		{
			var ret = await dialogManager.ShowDialogAsync(
				initializer,
				dialogType,
				selection => onResult(selection.Content as string),
				selections?.Any() == true
					? selections?.Select((x, i) => new DialogSelection {
						Id = i,
						Content = x,
						IsDefault = (i == 0)
					}).ToArray()
					: new DialogSelection[] { OkSelection(0, true) }
			);
			return ret?.Content as string;
		}
	}
}
