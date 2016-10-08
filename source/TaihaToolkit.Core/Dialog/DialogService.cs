using System;
using System.Globalization;
using Studiotaiha.Toolkit.Composition;

namespace Studiotaiha.Toolkit.Dialog
{
	public sealed class DialogService
	{
		#region Singleton

		static DialogService instance_;
		public static DialogService Instance => instance_ ?? (instance_ = new DialogService());

		#endregion

		/// <summary>
		/// Gets current dialog manager
		/// </summary>
		public IDialogManager DialogManager { get; private set; }

		private DialogService()
		{
			RecreateManager();
			if (DialogManager == null) { throw new InvalidOperationException("Failed to create alert manager"); }
		}

		public void RecreateManager()
		{			
			var loader = new ComponentImplementationLoader<IDialogManager>();
			DialogManager = loader.CreateInstance();
			
			DialogManagerChanged?.Invoke(this, DialogManager);
		}

		public event EventHandler<IDialogManager> DialogManagerChanged;
	}
}
