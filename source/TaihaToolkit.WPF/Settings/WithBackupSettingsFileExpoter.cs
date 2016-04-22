using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Studiotaiha.Toolkit.Settings;

namespace Studiotaiha.Toolkit.Settings
{
	public class WithBackupSettingsFileExpoter
	{
		public ISettingsContainer Container { get; }
		public ISettingsSerializer Serializer { get; }
		public string TargetFilePath { get; }
		public string TempFilePath { get; }

		public WithBackupSettingsFileExpoter(
			string targetFilePath,
			string tempFilePath,
			ISettingsContainer container,
			ISettingsSerializer serializer)
		{
			if (targetFilePath == null) { throw new ArgumentNullException(nameof(targetFilePath)); }
			if (tempFilePath == null) { throw new ArgumentNullException(nameof(tempFilePath)); }
			if (container == null) { throw new ArgumentNullException(nameof(container)); }
			if (serializer == null) { throw new ArgumentNullException(nameof(serializer)); }

			TargetFilePath = targetFilePath;
			TempFilePath = tempFilePath;
			Container = container;
		}
		public WithBackupSettingsFileExpoter(
			string targetFilePath,
			ISettingsContainer container,
			ISettingsSerializer serializer)
			: this(targetFilePath, targetFilePath + ".bak", container, serializer)
		{}

		public void Export()
		{
			using (var fs = new FileStream(TempFilePath, FileMode.Create, FileAccess.Write, FileShare.Read)) {
				Serializer.Serialize(fs, Container);
			}

			File.Copy(TempFilePath, TargetFilePath, true);
		}
	}
}
