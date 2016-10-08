using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Studiotaiha.Toolkit.ConsoleUtils;
using StudioTaiha.Toolkit.Showcase.ConsoleApp.Scenarios;

namespace TaihaToolkit.Showcase.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			try {
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
					SelectScenario(
						rootScenarios,
						input => {
							switch (input.ToLower()) {
								case "quit":
								case "exit":
									return false;

								default:
									return true;
							}									
						}
						);

					foreach (var scenario in rootScenarios) {
						Console.WriteLine(scenario.Title);
					}
					//var input = "";
					//do {
					//	input = Console.ReadLine();
					//}
					//while (input.ToLower() == "quit");
				}
			}
			catch (Exception ex) {
				using (new ForegroundColorScope(ConsoleColor.Red)) {
					Console.Error.WriteLine(ex.ToString());
				}
			}

			Console.Write("Press any key to exit...");
			Console.ReadKey();
		}

		static IScenario SelectScenario(
			IEnumerable<IScenario> scenarios,
			Func<string, bool> func,
			params string[] otherOptions)
		{
			while (true) {
				var input = Console.ReadLine();
				if (!func(input)) {
					break;
				}
			}
		}
	}
}
