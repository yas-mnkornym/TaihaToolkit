using System;

namespace Studiotaiha.Toolkit.Dialog
{
	/// <summary>
	/// An implementation of IDialogCallback to reroute its methods to delegates.
	/// </summary>
	public sealed class DelegateDialogCallback<TResult> : IDialogCallback<TResult>
	{
		/// <summary>
		/// Gets or sets the handler to handle OnClosing().
		/// </summary>
		public Func<TResult, bool> ClosingHandler { get; set; }

		/// <summary>
		/// Gets or sets the handler to handle OnClosed().
		/// </summary>
		public Action<TResult> ClosedHandler { get; set; }
		
		/// <summary>
		/// Gets or sets the handler to handle OnCopy().
		/// </summary>
		/// <remarks>
		/// SupportsCopy property returns true if the handler is not null, or false if the handler is null.
		/// </remarks>
		public Action CopyHandler { get; set; }

		#region IDialogCallback interface

		public void OnClosed(TResult selection) => ClosedHandler?.Invoke(selection);		

		public bool OnClosing(TResult selection) => ClosingHandler?.Invoke(selection) ?? true;

		public void OnCopy() => CopyHandler?.Invoke();

		public bool SupportsCopy => CopyHandler != null;

		#endregion // IDialogCallback interface
	}
}
