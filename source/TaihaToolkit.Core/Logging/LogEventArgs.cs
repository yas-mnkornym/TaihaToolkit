using System;

namespace Studiotaiha.Toolkit
{
	/// <summary>
	/// ログ追加イベント情報
	/// </summary>
	public sealed class LogEventArgs : EventArgs
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="logData">ログデータ</param>
		public LogEventArgs(LogData logData)
		{
			LogData = logData;
		}

		/// <summary>
		/// ログデータを取得する。
		/// </summary>
		public LogData LogData { get; private set; }
	}
}
