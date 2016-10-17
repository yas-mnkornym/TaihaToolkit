using System.Collections.Generic;

namespace Studiotaiha.Toolkit.Dialog
{
	class DialogConfig : IDialogConfig<DialogSelection>
	{
		public IDialogCallback<DialogSelection> Callback { get; set; }

		public string Caption { get; set; }

		public object Content { get; set; }

		public IDispatcher Dispatcher => null;

		public bool IsCopySupported { get; set; }

		public IEnumerable<DialogSelection> Selections { get; set; }
	}
}
