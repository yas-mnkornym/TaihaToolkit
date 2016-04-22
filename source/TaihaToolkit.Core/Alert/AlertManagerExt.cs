using System;

namespace Studiotaiha.Toolkit.Alert
{
	public static class AlertManagerExt
	{

		static IAlertStringProvider stringProvider_ = new EnglishStringProvider();

		/// <summary>
		/// 文字列プロバイダを設定する。
		/// </summary>
		public static IAlertStringProvider StringProvider
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
		/// <summary>
		/// 注意メッセージを表示する。
		/// </summary>
		/// <param name="alertManager">アラートマネージャ</param>
		/// <param name="message">メッセージ</param>
		/// <param name="caption">キャプション</param>
		/// <param name="ex">例外情報</param>
		/// <remarks>ユーザが選択しを選択するまで現在のスレッドをブロックする。</remarks>
		public static void ShowErrorMessage(
			this IAlertManager alertManager,
			string message,
			string caption = null,
			Exception ex = null)
		{
			alertManager.ShowMessage(message, caption ?? StringProvider.Error, ex, EAlertType.Error);
		}

		/// <summary>
		/// 注意メッセージを表示する。
		/// </summary>
		/// <param name="alertManager">アラートマネージャ</param>
		/// <param name="message">メッセージ</param>
		/// <param name="ex">例外情報</param>
		/// <remarks>ユーザが選択しを選択するまで現在のスレッドをブロックする。</remarks>
		public static void ShowErrorMessage(
			this IAlertManager alertManager,
			string message,
			Exception ex)
		{
			alertManager.ShowErrorMessage(message, null, ex);
		}

		/// <summary>
		/// 注意メッセージを表示する。
		/// </summary>
		/// <param name="alertManager">アラートマネージャ</param>
		/// <param name="message">メッセージ</param>
		/// <param name="caption">キャプション</param>
		/// <param name="ex">例外情報</param>
		/// <param name="onOk">ユーザが選択肢を選択した時の処理</param>
		/// <remarks>現在のスレッドをブロックせず、処理が続行する。</remarks>
		public static void PostErrorMessage(
			this IAlertManager alertManager,
			string message,
			string caption = null,
			Exception ex = null,
			Action onOk = null)
		{
			alertManager.PostMessage(message, caption ?? stringProvider_.Error, ex, EAlertType.Error, (selection) => { if (onOk != null) { onOk(); } });
		}

		/// <summary>
		/// 注意メッセージを表示する。
		/// </summary>
		/// <param name="alertManager">アラートマネージャ</param>
		/// <param name="message">メッセージ</param>
		/// <param name="ex">例外情報</param>
		/// <param name="onOk">ユーザが選択肢を選択した時の処理</param>
		/// <remarks>現在のスレッドをブロックせず、処理が続行する。</remarks>
		public static void PostErrorMessage(
			this IAlertManager alertManager,
			string message,
			Exception ex,
			Action onOk = null)
		{
			alertManager.PostErrorMessage(message, null, ex, onOk);
		}
		#endregion // ErrorMessage

		#region WarningMessage
		/// <summary>
		/// 警告メッセージを表示する。
		/// </summary>
		/// <param name="alertManager">アラートマネージャ</param>
		/// <param name="message">メッセージ</param>
		/// <param name="caption">キャプション</param>
		/// <param name="ex">例外情報</param>
		/// <remarks>ユーザが選択しを選択するまで現在のスレッドをブロックする。</remarks>
		public static void ShowWarningMessage(
			this IAlertManager alertManager,
			string message,
			string caption = null,
			Exception ex = null)
		{
			alertManager.ShowMessage(message, caption ?? StringProvider.Warning, ex, EAlertType.Warning);
		}

		/// <summary>
		/// 警告メッセージを表示する。
		/// </summary>
		/// <param name="alertManager">アラートマネージャ</param>
		/// <param name="message">メッセージ</param>
		/// <param name="ex">例外情報</param>
		/// <remarks>ユーザが選択しを選択するまで現在のスレッドをブロックする。</remarks>
		public static void ShowWarningMessage(
			this IAlertManager alertManager,
			string message,
			Exception ex)
		{
			alertManager.ShowWarningMessage(message, null, ex);
		}

		/// <summary>
		/// 警告メッセージを表示する。
		/// </summary>
		/// <param name="alertManager">アラートマネージャ</param>
		/// <param name="message">メッセージ</param>
		/// <param name="caption">キャプション</param>
		/// <param name="ex">例外情報</param>
		/// <param name="onOk">ユーザが選択肢を選択した時の処理</param>
		/// <remarks>現在のスレッドをブロックせず、処理が続行する。</remarks>
		public static void PostWarningMessage(
			this IAlertManager alertManager,
			string message,
			string caption = null,
			Exception ex = null,
			Action onOk = null)
		{
			alertManager.PostMessage(message, caption ?? StringProvider.Warning, ex, EAlertType.Warning, (selection) => { if (onOk != null) { onOk(); } });
		}

		/// <summary>
		/// 警告メッセージを表示する。
		/// </summary>
		/// <param name="alertManager">アラートマネージャ</param>
		/// <param name="message">メッセージ</param>
		/// <param name="ex">例外情報</param>
		/// <param name="onOk">ユーザが選択肢を選択した時の処理</param>
		/// <remarks>現在のスレッドをブロックせず、処理が続行する。</remarks>
		public static void PostWarningMessage(
			this IAlertManager alertManager,
			string message,
			Exception ex,
			Action onOk = null)
		{
			alertManager.PostWarningMessage(message, null, ex, onOk);
		}
		#endregion // WarningMessage

		#region ExclamationMessage
		/// <summary>
		/// 注意メッセージを表示する。
		/// </summary>
		/// <param name="alertManager">アラートマネージャ</param>
		/// <param name="message">メッセージ</param>
		/// <param name="caption">キャプション</param>
		/// <param name="ex">例外情報</param>
		/// <remarks>ユーザが選択しを選択するまで現在のスレッドをブロックする。</remarks>
		public static void ShowExclamationMessage(
			this IAlertManager alertManager,
			string message,
			string caption = null,
			Exception ex = null)
		{
			alertManager.ShowMessage(message, caption ?? StringProvider.Exclamation, ex, EAlertType.Exclamation);
		}

		/// <summary>
		/// 注意メッセージを表示する。
		/// </summary>
		/// <param name="alertManager">アラートマネージャ</param>
		/// <param name="message">メッセージ</param>
		/// <param name="ex">例外情報</param>
		/// <remarks>ユーザが選択しを選択するまで現在のスレッドをブロックする。</remarks>
		public static void ShowExclamationMessage(
			this IAlertManager alertManager,
			string message,
			Exception ex)
		{
			alertManager.ShowExclamationMessage(message, null, ex);
		}

		/// <summary>
		/// 注意メッセージを表示する。
		/// </summary>
		/// <param name="alertManager">アラートマネージャ</param>
		/// <param name="message">メッセージ</param>
		/// <param name="caption">キャプション</param>
		/// <param name="ex">例外情報</param>
		/// <param name="onOk">ユーザが選択肢を選択した時の処理</param>
		/// <remarks>現在のスレッドをブロックせず、処理が続行する。</remarks>
		public static void PostExclamationMessage(
			this IAlertManager alertManager,
			string message,
			string caption = null,
			Exception ex = null,
			Action onOk = null)
		{
			alertManager.PostMessage(message, caption ?? StringProvider.Exclamation, ex, EAlertType.Exclamation, (selection) => { if (onOk != null) { onOk(); } });
		}

		/// <summary>
		/// 注意メッセージを表示する。
		/// </summary>
		/// <param name="alertManager">アラートマネージャ</param>
		/// <param name="message">メッセージ</param>
		/// <param name="ex">例外情報</param>
		/// <param name="onOk">ユーザが選択肢を選択した時の処理</param>
		/// <remarks>現在のスレッドをブロックせず、処理が続行する。</remarks>
		public static void PostExclamationMessage(
			this IAlertManager alertManager,
			string message,
			Exception ex,
			Action onOk = null)
		{
			alertManager.PostExclamationMessage(message, null, ex, onOk);
		}
		#endregion // ExclamationMessage

		#region InformationMessage
		/// <summary>
		/// 情報メッセージを表示する。
		/// </summary>
		/// <param name="alertManager">アラートマネージャ</param>
		/// <param name="message">メッセージ</param>
		/// <param name="caption">キャプション</param>
		/// <param name="ex">例外情報</param>
		/// <remarks>ユーザが選択しを選択するまで現在のスレッドをブロックする。</remarks>
		public static void ShowInformationMessage(
			this IAlertManager alertManager,
			string message,
			string caption = null,
			Exception ex = null)
		{
			alertManager.ShowMessage(message, caption ?? StringProvider.Information, ex, EAlertType.Information);
		}

		/// <summary>
		/// 情報メッセージを表示する。
		/// </summary>
		/// <param name="alertManager">アラートマネージャ</param>
		/// <param name="message">メッセージ</param>
		/// <param name="ex">例外情報</param>
		/// <remarks>ユーザが選択しを選択するまで現在のスレッドをブロックする。</remarks>
		public static void ShowInformationMessage(
			this IAlertManager alertManager,
			string message,
			Exception ex)
		{
			alertManager.ShowInformationMessage(message, null, ex);
		}

		/// <summary>
		/// 情報メッセージを表示する。
		/// </summary>
		/// <param name="alertManager">アラートマネージャ</param>
		/// <param name="message">メッセージ</param>
		/// <param name="caption">キャプション</param>
		/// <param name="ex">例外情報</param>
		/// <param name="onOk">ユーザが選択肢を選択した時の処理</param>
		/// <remarks>現在のスレッドをブロックせず、処理が続行する。</remarks>
		public static void PostInformationMessage(
			this IAlertManager alertManager,
			string message,
			string caption = null,
			Exception ex = null,
			Action onOk = null)
		{
			alertManager.PostMessage(message, caption ?? StringProvider.Information, ex, EAlertType.Information, (selection) => { if (onOk != null) { onOk(); } });
		}

		/// <summary>
		/// 情報メッセージを表示する。
		/// </summary>
		/// <param name="alertManager">アラートマネージャ</param>
		/// <param name="message">メッセージ</param>
		/// <param name="ex">例外情報</param>
		/// <param name="onOk">ユーザが選択肢を選択した時の処理</param>
		/// <remarks>現在のスレッドをブロックせず、処理が続行する。</remarks>
		public static void PostInformationMessage(
			this IAlertManager alertManager,
			string message,
			Exception ex,
			Action onOk = null)
		{
			alertManager.PostInformationMessage(message, null, ex, onOk);
		}
		#endregion // InformationMessage

		#region DebugMessage
		/// <summary>
		/// デバッグメッセージを表示する。
		/// </summary>
		/// <param name="alertManager">アラートマネージャ</param>
		/// <param name="message">メッセージ</param>
		/// <param name="caption">キャプション</param>
		/// <param name="ex">例外情報</param>
		/// <remarks>ユーザが選択しを選択するまで現在のスレッドをブロックする。</remarks>
		public static void ShowDebugMessage(
			this IAlertManager alertManager,
			string message,
			string caption = null,
			Exception ex = null)
		{
			alertManager.ShowMessage(message, caption ?? StringProvider.Debug, ex, EAlertType.Debug);
		}

		/// <summary>
		/// デバッグメッセージを表示する。
		/// </summary>
		/// <param name="alertManager">アラートマネージャ</param>
		/// <param name="message">メッセージ</param>
		/// <param name="ex">例外情報</param>
		/// <remarks>ユーザが選択しを選択するまで現在のスレッドをブロックする。</remarks>
		public static void ShowDebugMessage(
			this IAlertManager alertManager,
			string message,
			Exception ex)
		{
			alertManager.ShowDebugMessage(message, null, ex);
		}

		/// <summary>
		/// デバッグメッセージを表示する。
		/// </summary>
		/// <param name="alertManager">アラートマネージャ</param>
		/// <param name="message">メッセージ</param>
		/// <param name="caption">キャプション</param>
		/// <param name="ex">例外情報</param>
		/// <param name="onOk">ユーザが選択肢を選択した時の処理</param>
		/// <remarks>現在のスレッドをブロックせず、処理が続行する。</remarks>
		public static void PostDebugMessage(
			this IAlertManager alertManager,
			string message,
			string caption = null,
			Exception ex = null,
			Action onOk = null)
		{
			alertManager.PostMessage(message, caption ?? StringProvider.Debug, ex, EAlertType.Debug, (selection) => { if (onOk != null) { onOk(); } });
		}

		/// <summary>
		/// デバッグメッセージを表示する。
		/// </summary>
		/// <param name="alertManager">アラートマネージャ</param>
		/// <param name="message">メッセージ</param>
		/// <param name="ex">例外情報</param>
		/// <param name="onOk">ユーザが選択肢を選択した時の処理</param>
		/// <remarks>現在のスレッドをブロックせず、処理が続行する。</remarks>
		public static void PostDebugMessage(
			this IAlertManager alertManager,
			string message,
			Exception ex,
			Action onOk = null)
		{
			alertManager.PostDebugMessage(message, null, ex, onOk);
		}
		#endregion // DebugMessage

		#region Question
		public static bool AskYesNo(
			this IAlertManager alertManager,
			string message,
			string caption = null,
			Exception ex = null)
		{
			var yes = StringProvider.Yes;
			var no = StringProvider.No;
			var ret = alertManager.ShowMessage(message,
				caption ?? StringProvider.Confirmation,
				ex, EAlertType.Question,
				yes, no);
			return (ret == yes);
		}

		public static void PostAskYesNo(
			this IAlertManager alertManager,
			string message,
			string caption = null,
			Exception ex = null,
			Action<bool> onSelected = null)
		{
			var yes = StringProvider.Yes;
			var no = StringProvider.No;
			alertManager.PostMessage(message,
				caption ?? StringProvider.Confirmation,
				ex, EAlertType.Question,
				selection => { if (onSelected != null) { onSelected(selection == yes); } },
				yes, no);
		}

		public static bool AskOkCancel(
			this IAlertManager alertManager,
			string message,
			string caption = null,
			Exception ex = null)
		{
			var ok = StringProvider.Ok;
			var cancel = StringProvider.Cancel;
			var ret = alertManager.ShowMessage(message,
				caption ?? StringProvider.Confirmation,
				ex, EAlertType.Question,
				ok, cancel);
			return (ret == ok);
		}

		public static void PostAskOkCancel(
			this IAlertManager alertManager,
			string message,
			string caption = null,
			Exception ex = null,
			Action<bool> onSelected = null)
		{
			var ok = StringProvider.Ok;
			var cancel = StringProvider.Cancel;
			alertManager.PostMessage(message,
				caption ?? StringProvider.Confirmation,
				ex, EAlertType.Question,
				selection => { if (onSelected != null) { onSelected(selection == ok); } },
				ok, cancel);
		}

		public static bool? AskYesNoCancel(
			this IAlertManager alertManager,
			string message,
			string caption = null,
			Exception ex = null)
		{
			var yes = StringProvider.Yes;
			var no = StringProvider.No;
			var cancel = StringProvider.Cancel;
			var ret = alertManager.ShowMessage(message,
				caption ?? StringProvider.Confirmation,
				ex, EAlertType.Question,
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

		public static void PostAskYesNoCancel(
			this IAlertManager alertManager,
			string message,
			string caption = null,
			Exception ex = null,
			Action<bool?> onSelected = null)
		{
			var yes = StringProvider.Yes;
			var no = StringProvider.No;
			var cancel = StringProvider.Cancel;
			alertManager.PostMessage(message,
				caption ?? StringProvider.Confirmation,
				ex, EAlertType.Question,
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
			this IAlertManager alertManager,
			string message,
			string caption = null,
			Exception ex = null,
			params string[] selections)
		{
			var ret = alertManager.ShowMessage(
				message,
				caption ?? StringProvider.Confirmation,
				ex, EAlertType.Question, selections);
			return ret;
		}

		public static string ShowSelections(
			this IAlertManager alertManager,
			string message,
			string caption = null,
			params string[] selections)
		{
			var ret = alertManager.ShowMessage(
				message,
				caption ?? StringProvider.Confirmation,
				null, EAlertType.Question, selections);
			return ret;
		}


		public static void PostSelections(
			this IAlertManager alertManager,
			string message,
			string caption = null,
			Exception ex = null,
			Action<string> onSelected = null,
			params string[] selections)
		{
			alertManager.PostMessage(
				   message,
				   caption ?? StringProvider.Confirmation,
				   ex, EAlertType.Question, onSelected, selections);
		}

		public static void PostSelections(
			this IAlertManager alertManager,
			string message,
			string caption = null,
			Action<string> onSelected = null,
			params string[] selections)
		{
			alertManager.PostMessage(
				   message,
				   caption ?? StringProvider.Confirmation,
				   null, EAlertType.Question, onSelected, selections);
		}
		#endregion
	}
}
