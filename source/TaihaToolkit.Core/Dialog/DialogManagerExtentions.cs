using System;
using System.Linq;
using System.Threading.Tasks;

namespace Studiotaiha.Toolkit.Dialog
{
	public static class AlertManagerExtentions
	{

		static IDialogStringLocalizer stringProvider_ = new DialogStringLocalizers.EnglishDialoStringLocalizer();

		/// <summary>
		/// 文字列プロバイダを設定する。
		/// </summary>
		public static IDialogStringLocalizer StringProvider
		{
			get
			{
				return stringProvider_;
			}
			internal set
			{
				stringProvider_ = value;
			}
		}

		#region ErrorMessage
		public static void ShowErrorMessage(
			this IDialogManager dialogManager,
			string message,
			string caption = null,
			Exception ex = null)
		{
			dialogManager.ShowMessage(message, caption ?? StringProvider.Error, ex, EDialogType.Error);
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
			return dialogManager.ShowMessageAsync(message, caption ?? stringProvider_.Error, ex, EDialogType.Error, (selection) => { if (onOk != null) { onOk(); } });
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
			dialogManager.ShowMessage(message, caption ?? StringProvider.Warning, ex, EDialogType.Warning);
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
			return dialogManager.ShowMessageAsync(message, caption ?? StringProvider.Warning, ex, EDialogType.Warning, (selection) => { if (onOk != null) { onOk(); } });
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
			dialogManager.ShowMessage(message, caption ?? StringProvider.Information, ex, EDialogType.Information);
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
			return dialogManager.ShowMessageAsync(message, caption ?? StringProvider.Information, ex, EDialogType.Information, (selection) => { if (onOk != null) { onOk(); } });
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
			dialogManager.ShowMessage(message, caption ?? StringProvider.Debug, ex, EDialogType.Debug);
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
			return dialogManager.ShowMessageAsync(message, caption ?? StringProvider.Debug, ex, EDialogType.Debug, (selection) => { if (onOk != null) { onOk(); } });
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
			var yes = StringProvider.Yes;
			var no = StringProvider.No;
			var ret = dialogManager.ShowMessage(message,
				caption ?? StringProvider.Confirmation,
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
			var yes = StringProvider.Yes;
			var no = StringProvider.No;
			return dialogManager.ShowMessageAsync(message,
				caption ?? StringProvider.Confirmation,
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
			var ok = StringProvider.Ok;
			var cancel = StringProvider.Cancel;
			var ret = dialogManager.ShowMessage(message,
				caption ?? StringProvider.Confirmation,
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
			var ok = StringProvider.Ok;
			var cancel = StringProvider.Cancel;
			return dialogManager.ShowMessageAsync(message,
				caption ?? StringProvider.Confirmation,
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
			var yes = StringProvider.Yes;
			var no = StringProvider.No;
			var cancel = StringProvider.Cancel;
			var ret = dialogManager.ShowMessage(message,
				caption ?? StringProvider.Confirmation,
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
			var yes = StringProvider.Yes;
			var no = StringProvider.No;
			var cancel = StringProvider.Cancel;
			return dialogManager.ShowMessageAsync(message,
				caption ?? StringProvider.Confirmation,
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
				caption ?? StringProvider.Confirmation,
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
				caption ?? StringProvider.Confirmation,
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
				   caption ?? StringProvider.Confirmation,
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
				   caption ?? StringProvider.Confirmation,
				   null, EDialogType.Question, onSelected, selections);
		}
		#endregion

		static DialogSelection OkSelection(int id, bool isDefault) => new DialogSelection {
			Id = id,
			Content = "OK",
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
