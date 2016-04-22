using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Toolkit.Alert
{
	public interface IDialogConfig
	{
		/// <summary>
		/// ダイアログが表示されるスレッドに関連づけられたディスパッチャを取得する。
		/// </summary>
		IDispatcher Dispatcher { get; }

		/// <summary>
		/// メインのコンテントを設定する。
		/// </summary>
		object Content { get; set; }

		/// <summary>
		/// ウィンドウのキャプションを設定する。
		/// </summary>
		/// <remarks>
		/// nullが設定された場合、規定のキャプションが利用される。
		/// </remarks>
		string Caption { get; set; }

		/// <summary>
		/// ダイアログのコールバックを設定する。
		/// </summary>
		IDialogCallback Callback { get; set; }

		/// <summary>
		/// ダイアログの選択肢を選択する。
		/// </summary>
		/// <remarks>
		/// nullか、空のコレクションが指定された場合にはShowDialog()関数で指定したボタン群か規定の値が利用される。
		/// </remarks>
		IEnumerable<string> Selections { get; set; }
	}
}
