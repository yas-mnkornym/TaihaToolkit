using System;
using System.Runtime.CompilerServices;

namespace Studiotaiha.Toolkit
{
	/// <summary>
	/// Loggerインターフェイス
	/// </summary>
	public interface ILogger
	{
		/// <summary>
		/// ロガーのタグを取得する
		/// </summary>
		string Tag { get; }

		/// <summary>
		/// ログを記録する
		/// </summary>
		/// <param name="message">メッセージ</param>
		/// <param name="level">ログレベル</param>
		/// <param name="exception">例外情報</param>
		/// <param name="file">ファイル名</param>
		/// <param name="line">行番号</param>
		/// <param name="member">メンバ名</param>
		void Log(
			string message,
			ELogLevel level = ELogLevel.Information,
			Exception exception = null,
			[CallerFilePath]string file = null,
			[CallerLineNumber]int line = 0,
			[CallerMemberName]string member = null
			);


		/// <summary>
		/// 子ロガーを作成する
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		ILogger CreateChild(string tag);

		/// <summary>
		/// 親ロガーを取得する
		/// </summary>
		ILogger Parent { get; }

		/// <summary>
		/// 最上位の親ロガーを表示する
		/// </summary>
		ILogger Root { get; }

		/// <summary>
		/// ログデータのサブジェクトを取得する。
		/// </summary>
		IObservable<LogData> LogSubject { get; }

		/// <summary>
		/// ログが追加されたことを通知するイベント
		/// </summary>
		/// <remarks>
		/// 下位のログデータの追加がすべて通知される。
		/// </remarks>
		event EventHandler<LogEventArgs> Logged;

	}
}
