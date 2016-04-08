using System.IO;

namespace Studiotaiha.Toolkit.WPF.Settings
{
	public interface ISettingsSerializer
	{
		/// <summary>
		/// 設定をシリアライズする
		/// </summary>
		/// <param name="stream">シリアライズすしたデータを記録するストリーム</param>
		/// <param name="settings">シリアライズする設定</param>
		void Serialize(Stream stream, SettingsImpl settings);

		/// <summary>
		/// 設定をデシリアライズする
		/// </summary>
		/// <param name="stream">デシリアライズするデータを読み込むストリーム</param>
		/// <param name="settings">デシリアライズしたデータを格納する設定</param>
		void Deserialize(Stream stream, SettingsImpl settings);
	}
}
