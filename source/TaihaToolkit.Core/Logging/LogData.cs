using System;

namespace Studiotaiha.Toolkit
{
	/// <summary>
	/// ログ情報
	/// </summary>
	public class LogData
	{
		/// <summary>
		/// タグ
		/// </summary>
		public string Tag { get; set; }

		/// <summary>
		/// 親タグ
		/// </summary>
		public string[] ParentTags { get; set; }

		/// <summary>
		/// メッセージ
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// ログレベル
		/// </summary>
		public ELogLevel Level { get; set; }

		/// <summary>
		/// 例外情報
		/// </summary>
		public Exception Exception { get; set; }

		/// <summary>
		/// ファイル名
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// 行番号
		/// </summary>
		public int LineNumber { get; set; }

		/// <summary>
		/// メンバ名
		/// </summary>
		public string MemberName { get; set; }
	}
}
