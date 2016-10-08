namespace Studiotaiha.Toolkit.Dialog.DialogStringLocalizers
{
	[DialogStringLocalizer("ja")]
	public class JapaneseDialoStringLocalizer : IDialogStringLocalizer
	{
		public string Error { get; } = "エラー";

		public string Warning { get; } = "警告";

		public string Information { get; } = "情報";

		public string Debug { get; } = "デバッグ";

		public string Question { get; } = "質問";

		public string Yes { get; } = "はい";

		public string No { get; } = "いいえ";

		public string Ok { get; } = "OK";

		public string Cancel { get; } = "キャンセル";

		public string Select { get; } = "選択";

		public string Confirmation { get; } = "確認";
	}
}