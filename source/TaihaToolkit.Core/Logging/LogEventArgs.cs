using System;

namespace Studiotaiha.Toolkit.Logging
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
		public LogEventArgs(LogEvent logData)
		{
			LogData = logData;
		}

		/// <summary>
		/// ログデータを取得する。
		/// </summary>
		public LogEvent LogData { get; private set; }
	}
}
