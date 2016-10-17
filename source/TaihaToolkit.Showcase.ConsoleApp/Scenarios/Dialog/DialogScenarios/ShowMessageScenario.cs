using System;
using Studiotaiha.Toolkit.Dialog;

namespace StudioTaiha.Toolkit.Showcase.ConsoleApp.Scenarios.Dialog.DialogScenarios
{
	[SubScenario(typeof(DialogScenario))]
	class ShowMessageScenario : DialogScenarioBase
	{
		public ShowMessageScenario()
			: base("ShowMessage", "Show a dialog using ShowMessage method.")
		{ }

		protected override void ShowDialog(IDialogManager dialogMangaer)
		{
			dialogMangaer.ShowMessage(
				"Message body",
				"Caption",
				new Exception("exception message"),
				EDialogType.Information);
		}
	}
}
