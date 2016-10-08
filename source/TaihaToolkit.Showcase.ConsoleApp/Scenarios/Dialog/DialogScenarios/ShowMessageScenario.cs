using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Studiotaiha.Toolkit.Dialog;

namespace StudioTaiha.Toolkit.Showcase.ConsoleApp.Scenarios.Dialog.DialogScenarios
{
	class ShowMessageScenario : DialogScenarioBase
	{
		public ShowMessageScenario()
			: base("ShowMessage", "Show a dialog using ShowMessage method.")
		{ }

		protected override void ShowDialog(IDialogManager dialogMangaer)
		{
			dialogMangaer.ShowMessage(
				"Content",
				"Caption",
				new Exception("exception message"),
				EDialogType.Information);
		}
	}
}
