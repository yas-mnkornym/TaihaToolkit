using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Toolkit.Alert
{
	/// <summary>
	/// アラートを表示する
	/// </summary>
	public interface IAlertManager
	{		
		/// <summary>
		/// メッセージを表示し、ユーザの選択を待機する。
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="caption">キャプション</param>
		/// <param name="exception">例外情報</param>
		/// <param name="alertType">メッセージの種類</param>
		/// <param name="selections">選択肢（nullを指定すると「OK」が設定される）</param>
		/// <returns>ユーザの選んだ選択肢</returns>
		string ShowMessage(
			string message,
			string caption,
			Exception exception,
			EAlertType alertType,
			params string[] selections);

		/// <summary>
		/// メッセージを表示する（待機しない）
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="caption">キャプション</param>
		/// <param name="exception">例外情報</param>
		/// <param name="alertType">メッセージの種類</param>
		/// <param name="onResult">選択された時の処理</param>
		/// <param name="selections">選択肢（nullを指定すると「OK」が設定される）</param>
		void PostMessage(
			string message,
			string caption,
			Exception exception,
			EAlertType alertType,
			Action<string> onResult,
			params string[] selections);
		
		/// <summary>
		/// ダイアログを表示する。
		/// </summary>
		/// <param name="initializer"></param>
		/// <param name="alertType"></param>
		/// <param name="selections"></param>
		/// <returns></returns>
		string ShowDialog(
			Action<IDialogConfig> initializer,
			EAlertType alertType,
			params string[] selections);

		/// <summary>
		/// ダイアログを表示する。
		/// </summary>
		/// <param name="initializer"></param>
		/// <param name="alertType"></param>
		/// <param name="selections"></param>
		/// <returns></returns>
		 void PostDialog(
			Action<IDialogConfig> initializer,
			EAlertType alertType,
			Action<string> onResult,
			params string[] selections);

		/// <summary>
		/// キャプションバーに表示されるメッセージを設定する
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="kind">メッセージの種別</param>
		/// <remarks>改行は無視される。</remarks>
		 void SetCaptionMessage(
			 string message,
			 CaptionMessageKind kind = CaptionMessageKind.None);
	}

	/// <summary>
	/// アラート（メッセージ）の種類
	/// </summary>
	public enum EAlertType
	{
		/// <summary>
		/// 種類なし
		/// </summary>
		None,

		/// <summary>
		/// 情報メッセージ
		/// </summary>
		Information,

		/// <summary>
		/// デバッグメッセージ
		/// </summary>
		Debug,

		/// <summary>
		/// 警告メッセージ
		/// </summary>
		Warning,

		/// <summary>
		/// 注意メッセージ
		/// </summary>
		Exclamation,

		/// <summary>
		/// エラー情報
		/// </summary>
		Error,
		
		/// <summary>
		/// 質問
		/// </summary>
		Question,
	}
}
