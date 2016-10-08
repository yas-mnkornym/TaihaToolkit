using System;
using Studiotaiha.Toolkit.Dialog;

namespace StudioTaiha.Toolkit.Showcase.ConsoleApp.Scenarios.Dialog
{
	abstract class DialogScenarioBase : SubScenarioBase
	{
		protected DialogScenarioBase(
			string title,
			string description,
			bool isExecutable = true)
		{
			if (title == null) { throw new ArgumentNullException(nameof(title)); }
			if (description == null) { throw new ArgumentNullException(nameof(description)); }
			Title = title;
			Description = description;
			IsExecutable = isExecutable;
		}

		public override void Execute()
		{
			var dialogManager = DialogService.Instance.DialogManager;
			ShowDialog(dialogManager);
		}

		protected abstract void ShowDialog(IDialogManager dialogMangaer);
	}
}
