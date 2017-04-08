namespace Studiotaiha.Toolkit.Dialog
{
	public interface IDialogCallback<TResult>
	{
		/// <summary>
		/// Invoked before the dialog is closing.
		/// </summary>
		/// <param name="selection">Selected dialog item</param>
		/// <returns>
		/// Dialog will close if true is returned.
		/// </returns>
		bool OnClosing(TResult selection);

		/// <summary>
		/// Invoked after the dialog is closed.
		/// </summary>
		/// <param name="selection">Selected dialog item</param>
		void OnClosed(TResult selection);

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
