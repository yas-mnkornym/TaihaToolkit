using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Studiotaiha.Toolkit.Dialog;

namespace StudioTaiha.Toolkit.Showcase.ConsoleApp.Scenarios.Dialog
{
	abstract class DialogScenarioBase : IScenario
	{
		protected DialogScenarioBase(
			string title,
			string description)
		{
			if (title == null) { throw new ArgumentNullException(nameof(title)); }
			if (description == null) { throw new ArgumentNullException(nameof(description)); }
			Title = title;
			Description = description;
		}				

		public string Title { get; }
		public string Description { get; }

		public bool IsExecutable { get; } = true;

		public IEnumerable<IScenario> SubScenarios { get; } =
			Assembly.GetExecutingAssembly()
				.GetTypes()
				.Where(x => x.IsSubclassOf(typeof(DialogScenarioBase)))
				.Select(x => Activator.CreateInstance(x) as IScenario)
				.Where(x => x != null)
				.ToArray();			


		public void Execute()
		{
			var dialogManager = DialogService.Instance.DialogManager;
			ShowDialog(dialogManager);
		}

		protected abstract void ShowDialog(IDialogManager dialogMangaer);
	}
}
