namespace Studiotaiha.Toolkit.Dialog
{
	public interface IDialogStringLocalizer
	{
		/// <summary>
		/// Gets the string represents EAlertType.Error.
		/// </summary>
		string Error { get; }

		/// <summary>
		/// Gets the string represents EAlertType.Warning.
		/// </summary>
		string Warning { get; }

		/// <summary>
		/// Gets the string represents EAlertType.Information.
		/// </summary>
		string Information { get; }

		/// <summary>
		/// Gets the string represents EAlertType.Debug.
		/// </summary>
		string Debug { get; }

		/// <summary>
		/// Gets the string represents EAlertType.Question.
		/// </summary>
		string Question { get; }

		/// <summary>
		/// Gets the string represents "Yes".
		/// </summary>
		string Yes { get; }

		/// <summary>
		/// Gets the string represents "No".
		/// </summary>
		string No { get; }

		/// <summary>
		/// Gets the string represents "OK".
		/// </summary>
		string Ok { get; }

		/// <summary>
		/// Gets the string represents "Cancel".
		/// </summary>
		string Cancel { get; }

		/// <summary>
		/// Gets the string represents "Select".
		/// </summary>
		string Select { get; }

		/// <summary>
		/// Gets the string represents "Confirmation".
		/// </summary>
		string Confirmation { get; }
	}
}
