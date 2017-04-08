using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading.Tasks;

namespace Studiotaiha.Toolkit.Dialog
{
	/// <summary>
	/// アラートを表示する
	/// </summary>
	public interface IDialogManager : INotifyPropertyChanged
	{
		/// <summary>
		/// Show message and wait for it closes.
		/// </summary>
		/// <param name="message">Content</param>
		/// <param name="caption">Caption</param>
		/// <param name="exception">Related exception</param>
		/// <param name="dialogType">Type of the dialog</param>
		/// <param name="onResult">Callback when a option is selected.</param>
		/// <param name="selections">options</param>
		string ShowMessage(
			string content,
			string caption,
			Exception exception,
			EDialogType dialogType,
			params string[] selections);

		/// <summary>
		/// Show message.
		/// </summary>
		/// <param name="content">Content</param>
		/// <param name="caption">Caption</param>
		/// <param name="exception">Related exception</param>
		/// <param name="dialogType">Type of the dialog</param>
		/// <param name="onResult">Callback when a option is selected.</param>
		/// <param name="selections">options</param>
		Task<string> ShowMessageAsync(
			string content,
			string caption,
			Exception exception,
			EDialogType dialogType,
			Action<string> onResult,
			params string[] selections);

		/// <summary>
		/// Show dialog and wait for it closes.
		/// </summary>
		/// <param name="initializer">Dialog initializer</param>
		/// <param name="dialogType">Alert type</param>
		/// <param name="selections">Selections</param>
		/// <returns></returns>
		DialogSelection ShowDialog(
			Action<IDialogConfig<DialogSelection>> initializer,
			EDialogType dialogType,
			params DialogSelection[] selections);

		/// <summary>
		/// Show dialog.
		/// </summary>
		/// <param name="initializer">Dialog initializer</param>
		/// <param name="dialogType">Alert type</param>
		/// <param name="selections">Selections</param>
		/// <returns></returns>
		 Task<DialogSelection> ShowDialogAsync(
			Action<IDialogConfig<DialogSelection>> initializer,
			EDialogType dialogType,
			Action<DialogSelection> onResult,
			params DialogSelection[] selections);
		
		/// <summary>
		/// Gets a new instance of a common selection.
		/// </summary>
		/// <param name="selectionType">Type of the common selection</param>
		/// <param name="id">ID to be set</param>
		/// <param name="isDefault">IsDefault value to be set</param>
		/// <returns>New instance of the common selection</returns>
		DialogSelection CreateCommonSelection(
			ECommonSelection selectionType,
			int id,
			bool isDefault = false);

		/// <summary>
		/// Sets or gets the current culture used to localize dialog strings.
		/// </summary>
		/// <remarks>
		/// Default culture is used instead if this is null.
		/// </remarks>
		CultureInfo Culture { get; set; }
	}
}
