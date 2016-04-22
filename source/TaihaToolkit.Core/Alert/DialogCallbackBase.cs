using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Toolkit.Alert
{
	/// <summary>
	/// ダイアログコールバックの基本実装
	/// </summary>
	public class DialogCallbackBase : IDialogCallback
	{
		bool supportsCopy_ = false;

		#region IDialogCallbackインターフェイス
		virtual public bool OnClosing(string selection)
		{
			return true;
		}

		virtual public void OnClosed()
		{ }

		virtual public void OnCopy()
		{ }

		/// <summary>
		/// コピーをサポートするかを示すフラグを設定・取得する
		/// </summary>
		public bool SupportsCopy
		{
			get
			{
				return supportsCopy_;
			}
			set
			{
				supportsCopy_ = value;
			}
		}
		#endregion
	}
}
