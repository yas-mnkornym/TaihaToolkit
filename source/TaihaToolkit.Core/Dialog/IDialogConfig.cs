using System.Collections.Generic;

namespace Studiotaiha.Toolkit.Dialog
{
	public interface IDialogConfig<TResult>
	{
		/// <summary>
		/// Gets dispatcher object associated to UI thread.
		/// </summary>
		IDispatcher Dispatcher { get; }

		/// <summary>
		/// Sets or gets content of the dialog.
		/// </summary>
		object Content { get; set; }

		/// <summary>
		/// Sets or  gets caption of the dialog.
		/// </summary>
		string Caption { get; set; }

		/// <summary>
		/// Gets or sets callback of the dialog.
		/// </summary>
		IDialogCallback<TResult> Callback { get; set; }

		/// <summary>
		/// Sets or gets selections of the dialog.
		/// </summary>
		IEnumerable<DialogSelection> Selections { get; set; }
	}
}
