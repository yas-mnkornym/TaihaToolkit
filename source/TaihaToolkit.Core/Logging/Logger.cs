using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace Studiotaiha.Toolkit
{

	internal sealed class Logger : ILogger, IDisposable
	{
		string[] ParentTags { get; }
		Subject<LogData> Subject { get; } = new Subject<LogData>();

		public string Tag { get; set; }

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="tag">タグ</param>
		public Logger(string tag)
		{
			if (tag == null) { throw new ArgumentNullException("tag"); }
			Tag = tag;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="tag">タグ</param>
		/// <param name="parent">親ロガー</param>
		public Logger(string tag, ILogger parent)
			: this(tag)
		{
			if (parent == null) { throw new ArgumentNullException("parent"); }
			Parent = parent;

			// 再帰的に親タグを取得
			var logger = parent;
			List<string> tags = new List<string>();
			do {
				tags.Add(logger.Tag);
				logger = logger.Parent;
			}
			while (logger != null);

			// ルートを基点に並び替えておく
			tags.Reverse();
			ParentTags = tags.ToArray();
		}

		public void Log(string message, ELogLevel level = ELogLevel.Information, Exception exception = null, string file = null, int line = 0, string member = null)
		{
			var data = new LogData {
				Message = message,
				Level = level,
				Exception = exception,
				FileName = file,
				LineNumber = line,
				MemberName = member,
				Tag = Tag,
				ParentTags = ParentTags
			};

			// Rxで通知
			Subject.OnNext(data);

			// イベントで通知
			if (Logged != null) {
				Logged(this, new LogEventArgs(data));
			}
		}

		public ILogger CreateChild(string tag)
		{
			var logger = new Logger(tag, this);
			logger.Logged += (_, e) => {
				Subject.OnNext(e.LogData);
				if (Logged != null) {
					Logged(this, new LogEventArgs(e.LogData));
				}
			};
			return logger;
		}

		public ILogger Parent
		{
			get;
			set;
		}

		ILogger root_ = null;
		public ILogger Root
		{
			get
			{
				if (root_ == null) {
					var logger = Parent;
					while (logger != null && logger.Parent != null) {
						logger = logger.Parent;
					}
					root_ = logger;
				}
				return root_;
			}
		}

		public IObservable<LogData> LogSubject
		{
			get
			{
				return Subject;
			}
		}

		public event EventHandler<LogEventArgs> Logged;

		#region IDisposable メンバ
		bool isDisposed_ = false;
		void Dispose(bool disposing)
		{
			if (isDisposed_) { return; }
			if (disposing) {
				if (Subject != null) {
					Subject.Dispose();
				}
			}
			isDisposed_ = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}
