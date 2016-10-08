using System;
using Studiotaiha.Toolkit.ConsoleUtils;
using Studiotaiha.Toolkit.Dialog;

namespace StudioTaiha.Toolkit.Showcase.ConsoleApp.Scenarios.Dialog.DialogScenarios
{
	[SubScenario(typeof(DialogScenario))]
	class ShowDialogScenario : DialogScenarioBase
	{
		public ShowDialogScenario()
			: base("ShowDialog", "Show a dialog using ShowDialog method.")
		{ }

		protected override void ShowDialog(IDialogManager dialogMangaer)
		{
			var ret = dialogMangaer.ShowDialog(config => {
				config.Caption = "Dialog Title";
				config.Content = "Select one.";
				config.IsCopySupported = false;
				config.Callback = new DelegateDialogCallback<DialogSelection> {
					ClosingHandler = selection => {
						using (new ForegroundColorScope(ConsoleColor.DarkYellow)) {
							Console.WriteLine("Dialog closing. Id: {0}, Content: {1}, IsDefault: {2}",
								selection.Id,
								selection.Content,
								selection.IsDefault);
						}
						return true;
					},
					ClosedHandler = selection => {
						using (new ForegroundColorScope(ConsoleColor.DarkYellow)) {
							Console.WriteLine("Dialog closed. Id: {0}, Content: {1}, IsDefault: {2}",
								selection.Id,
								selection.Content,
								selection.IsDefault);
						}
					},
				};
			},
			EDialogType.Question,
			"One", "Two", "Three");

			using (new ForegroundColorScope(ConsoleColor.Green)) {
				Console.WriteLine("Selected: {0}", ret);
			}
		}
	}
}
