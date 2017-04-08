namespace Studiotaiha.Toolkit.Dialog
{
	/// <summary>
	/// A base implementation of IDialogCallback
	/// </summary>
	public class DialogCallbackBase<TResult> : IDialogCallback<TResult>
	{
		public virtual bool OnClosing(TResult selection) => true;

		public virtual void OnClosed(TResult selection) { }

		public virtual void OnCopy() { }
		
		public bool SupportsCopy { get; set; }
	}
}
