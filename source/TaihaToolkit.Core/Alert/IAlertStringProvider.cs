using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Toolkit
{
	public interface IAlertStringProvider
	{
		/// <summary>
		/// エラー
		/// </summary>
		string Error { get; }

		/// <summary>
		/// 警告
		/// </summary>
		string Warning { get; }

		/// <summary>
		/// 情報
		/// </summary>
		string Information { get; }

		/// <summary>
		/// デバッグ
		/// </summary>
		string Debug { get; }

		/// <summary>
		/// 質問
		/// </summary>
		string Question { get; }

		/// <summary>
		/// はい
		/// </summary>
		string Yes { get; }

		/// <summary>
		/// いいえ
		/// </summary>
		string No { get; }

		/// <summary>
		/// OK
		/// </summary>
		string Ok { get; }

		/// <summary>
		/// キャンセル
		/// </summary>
		string Cancel { get; }

		/// <summary>
		/// 選択
		/// </summary>
		string Select { get; }

		/// <summary>
		/// 確認
		/// </summary>
		string Confirmation { get; }

		/// <summary>
		/// 注意
		/// </summary>
		string Exclamation { get; }
	}
}
