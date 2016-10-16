using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StudioTaiha.Toolkit.Showcase.ConsoleApp.Scenarios.Dialog
{
	[RootScenario]
	class DialogScenario : IScenario
	{
		public string Description { get; } = "Show samples of DialogManager.";

		public bool IsExecutable { get; } = false;

		public string Title { get; } = "Dialog";

		public void Execute()
		{
			throw new NotSupportedException();
		}

		public IEnumerable<IScenario> SubScenarios { get; } =
			Assembly.GetExecutingAssembly()
				.GetTypes()
				.Where(type => type.IsSubclassOf(typeof(DialogScenarioBase)))
				.Select(type => (IScenario)Activator.CreateInstance(type));
	}
}
