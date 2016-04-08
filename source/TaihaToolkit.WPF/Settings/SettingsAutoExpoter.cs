using System;
using System.IO;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Studiotaiha.Toolkit.WPF.Settings
{
	public class SettingsAutoExpoter : IDisposable
	{
		#region Private Field
		ISettingsSerializer Serializer { get; }
		Stream Stream { get; }
		CompositeDisposable Disposables { get; } = new CompositeDisposable();
		#endregion

		public SettingsAutoExpoter(
			string filePath,
			string tempFilePath,
			SettingsImpl settings,
			ISettingsSerializer serializer,
			int delay = 300)
		{
			if (filePath == null) { throw new ArgumentNullException("filePath"); }
			if (tempFilePath == null) { throw new ArgumentNullException("tempFilePath"); }
			if (settings == null) { throw new ArgumentNullException("settings"); }
			if (delay < 0) { throw new ArgumentOutOfRangeException("dealy must be >= 0)"); }
			if (serializer == null) { throw new ArgumentNullException("serializer"); }
			Serializer = serializer;

			// ファイルを開く
			Stream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write, FileShare.Read);
			Disposables.Add(Stream);

			// 設定変更を監視
			var subscription = Observable.FromEventPattern<SettingChangeEventArgs>(settings, "SettingChanged")
				.Throttle(TimeSpan.FromMilliseconds(delay))
				.Subscribe(args => {
					try {
						Export(filePath, tempFilePath, settings);
						Exported?.Invoke(this, new EventArgs());
					}
					catch (Exception ex) {
						Error?.Invoke(this, new ErrorEventArgs(ex));
					}
				});
			Disposables.Add(subscription);
		}

		void Export(
			string filePath,
			string tempFilePath,
			SettingsImpl settings
			)
		{
			if (filePath == null) { throw new ArgumentNullException("filePath"); }
			if (tempFilePath == null) { throw new ArgumentNullException("tempFilePath"); }
			if (settings == null) { throw new ArgumentNullException("settings"); }

			// ファイルを空にする
			Stream.SetLength(0);

			// 設定をシリアラズしてファイルに保存
			Serializer.Serialize(Stream, settings);

			// 一時ファイルから本来のファイルにコピーする
			File.Copy(tempFilePath, filePath, true);
		}

		public event EventHandler Exported;
		public event EventHandler<ErrorEventArgs> Error;

		#region IDisposable メンバ
		bool isDisposed_ = false;
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "stream_")]
		virtual protected void Dispose(bool disposing)
		{
			if (isDisposed_) { return; }
			if (disposing) {
				Disposables.Dispose();
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
