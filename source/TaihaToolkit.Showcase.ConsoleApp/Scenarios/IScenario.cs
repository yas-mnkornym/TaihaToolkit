using System.Collections.Generic;

namespace StudioTaiha.Toolkit.Showcase.ConsoleApp.Scenarios
{
	public interface IScenario
	{
		string Title { get; }
		string Description { get; }

		bool IsExecutable { get; }
		void Execute();
		
		IEnumerable<IScenario> SubScenarios { get; }
	}
}
