namespace Studiotaiha.Toolkit.Dialog
{
	/// <summary>
	/// A base implementation of IDialogCallback
	/// </summary>
	public class DialogCallbackBase<TResult> : IDialogCallback<TResult>
	{
		virtual public bool OnClosing(TResult selection) => true;

		virtual public void OnClosed() { }

		virtual public void OnCopy() { }
		
		public bool SupportsCopy { get; set; }
	}
}
