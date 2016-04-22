using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Studiotaiha.Toolkit.Alert
{
	/// <summary>
	/// ダイアログコールバックインターフェイス
	/// </summary>
	public interface IDialogCallback
	{
		/// <summary>
		/// ウィンドウが閉じる前(選択肢が選択された時)に呼び出される。
		/// </summary>
		/// <param name="selection"></param>
		/// <returns>trueを返せばウィンドウが閉じる。falseを返せばウィンドウが残る。</returns>
		bool OnClosing(string selection);

		/// <summary>
		/// ウィンドウが閉じた直後によびだされる。
		/// </summary>
		void OnClosed();

		/// <summary>
		/// コピーボタンが押された時に呼び出される。
		/// </summary>
		void OnCopy();

		/// <summary>
		/// ダイアログがコピーをサポートしているかどうかを表すフラグを取得する。
		/// </summary>
		bool SupportsCopy { get; }
	}
}
