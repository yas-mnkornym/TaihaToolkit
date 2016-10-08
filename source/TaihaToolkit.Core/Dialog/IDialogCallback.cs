namespace Studiotaiha.Toolkit.Dialog
{
	public interface IDialogCallback<TResult>
	{
		/// <summary>
		/// Invoked before the dialog is closing.
		/// </summary>
		/// <param name="selection"></param>
		/// <returns>
		/// Dialog will close if true is returned.
		/// </returns>
		bool OnClosing(TResult selection);

		/// <summary>
		/// Invoked after the dialog is closed.
		/// </summary>
		void OnClosed();

		/// <summary>
		/// Invoked when the copy button is pressed.
		/// </summary>
		void OnCopy();

		/// <summary>
		/// Gets the value which represents whether the dialog supports copy or not.
		/// </summary>
		bool SupportsCopy { get; }
	}
}
