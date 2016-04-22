using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Toolkit.Alert
{
	/// <summary>
	/// DialogCallbackの処理をDelegateで行うためのクラス
	/// </summary>
	public sealed class DelegateDialogCallback : IDialogCallback
	{
		/// <summary>
		/// OnClosingの処理を行うハンドラを設定・取得する
		/// </summary>
		public Func<string, bool> ClosingHandler { get; set; }

		/// <summary>
		/// OnClosedの処理を行うハンドラを設定・取得する
		/// </summary>
		public Action ClosedHandler { get; set; }

		/// <summary>
		/// OnCopy()の処理を行うハンドラを設定・取得する
		/// </summary>
		/// <remarks>このハンドラがnullならSupportsCopyはfalseを返し、nullでなければtrueを返す。</remarks>
		public Action CopyHandler { get; set; }

		#region IDialogCallbackインターフェイス
		public void OnClosed()
		{
			if (ClosedHandler != null) {
				ClosedHandler();
			}
		}

		public bool OnClosing(string selection)
		{
			if (ClosingHandler != null) {
				return ClosingHandler(selection);
			}
			return true;
		}

		public void OnCopy()
		{
			if (CopyHandler != null) {
				CopyHandler();
			}
		}

		public bool SupportsCopy
		{
			get
			{
				return (CopyHandler != null);
			}
		}
		#endregion // IDialogCallbackインターフェイス
	}
}
