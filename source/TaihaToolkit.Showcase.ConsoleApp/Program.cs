using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Studiotaiha.Toolkit;
using Studiotaiha.Toolkit.ConsoleUtils;
using StudioTaiha.Toolkit.Showcase.ConsoleApp.Scenarios;

namespace TaihaToolkit.Showcase.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			try {
                Studiotaiha.Toolkit.TaihaToolkit.Current.RegisterComponent(ConsoleComponent.Instance);

				var rootScenarios = Assembly.GetExecutingAssembly()
					.GetTypes()
					.Where(x => x.GetInterface(typeof(IScenario).FullName) != null)
					.Select(x => new { Type = x, Attribute = x.GetCustomAttribute(typeof(RootScenarioAttribute)) })
					.Where(x => x.Attribute != null)
					.Select(x => (IScenario)Activator.CreateInstance(x.Type));

				if (!rootScenarios.Any()) {
					Console.WriteLine("No scenario available.");
				}
				else {
					var rootScenario = new RootScenario();
					rootScenario.SubScenarioList.AddRange(rootScenarios);

					SelectScenarioRecursively(rootScenario);
				}
			}
			catch (Exception ex) {
				using (new ForegroundColorScope(ConsoleColor.Red)) {
					Console.Error.WriteLine(ex.ToString());
				}
			}
		}

		static void SelectScenarioRecursively(IScenario scenario, bool isRoot = true)
		{	
			var scenarios = new List<IScenario>();

			// add scenario to back or exit
			var isRunning = true;
			if (isRoot) {
				var exitScenario = new ExitScenario();
				exitScenario.ExitRequested += (_, __) => isRunning = false;
				scenarios.Add(exitScenario);
			}
			else {
				var backScenario = new BackScenario();
				backScenario.BackRequested += (_, __) => isRunning = false;
				scenarios.Add(backScenario);
			}

			// add the scenario
			if (scenario.IsExecutable) {
				scenarios.Add(scenario);
			}

			// add sub scenarios
			if (scenario.SubScenarios?.Any() == true) {
				scenarios.AddRange(scenario.SubScenarios);
			}

			var menu = string.Format("[{0}] {1}",
				scenario.Title,
				string.Join(" ", scenarios.Select((x, i) => string.Format("{0}.{1}", i, x.Title))));

			while(isRunning) {
				Console.WriteLine(menu);
				Console.Write(">");
				var input = Console.ReadLine();

				int selectedIndex;
				if (!int.TryParse(input, out selectedIndex)) {
					continue;
				}
				else if (0 <= selectedIndex && selectedIndex < scenarios.Count) {
					var selectedScenario = scenarios[selectedIndex];

					if (selectedScenario.SubScenarios?.Any() == true) {
						SelectScenarioRecursively(selectedScenario, false);
					}
					else if (selectedScenario.IsExecutable) {
						try {
							selectedScenario.Execute();
						}
						catch (Exception ex) {
							using (new ForegroundColorScope(ConsoleColor.Red)) {
								Console.WriteLine("Failed to execute {0} scenario.", selectedScenario.Title);
								Console.WriteLine("*** Exception ***");
								Console.WriteLine(ex);
								Console.WriteLine();
							}
						}
					}
				}
			}
		}

		class BackScenario : IScenario
		{
			public string Description => "Back to the previous scenario";

			public bool IsExecutable => true;

			public IEnumerable<IScenario> SubScenarios => null;

			public string Title => "Back";

			public void Execute()
			{
				BackRequested?.Invoke(this, EventArgs.Empty);
			}

			public event EventHandler BackRequested;
		}

		class ExitScenario : IScenario
		{
			public string Description => "Exit the showcase";

			public bool IsExecutable => true;

			public IEnumerable<IScenario> SubScenarios => null;

			public string Title => "Exit";

			public void Execute()
			{
				ExitRequested?.Invoke(this, EventArgs.Empty);
			}

			public event EventHandler ExitRequested;
		}
		
		class RootScenario : IScenario
		{
			public string Description => "The root scenario.";
			public bool IsExecutable => false;

			public List<IScenario> SubScenarioList { get; } = new List<IScenario>();
			public IEnumerable<IScenario> SubScenarios => SubScenarioList;

			public string Title => "Root";

			public void Execute()
			{
				throw new NotSupportedException();
			}
		}
	}
}
