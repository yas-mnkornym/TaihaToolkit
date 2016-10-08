namespace Studiotaiha.Toolkit.Dialog.DialogStringLocalizers
{
	[DialogStringLocalizer("en", "en-us")]
	public class EnglishDialoStringLocalizer : IDialogStringLocalizer
	{
		public string Error { get; } = "Error";

		public string Warning { get; } = "Warning";

		public string Information { get; } = "Information";

		public string Debug { get; } = "Debug";

		public string Question { get; } = "Question";

		public string Yes { get; } = "Yes";

		public string No { get; } = "No";

		public string Ok { get; } = "OK";

		public string Cancel { get; } = "Cancel";

		public string Select { get; } = "Select";

		public string Confirmation { get; } = "Confirmation";
	}
}