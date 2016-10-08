using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioTaiha.Toolkit.Showcase.ConsoleApp.Scenarios
{
	interface IScenario
	{
		string Title { get; }
		string Description { get; }

		bool IsExecutable { get; }
		void Execute();
		
		IEnumerable<IScenario> SubScenarios { get; }
	}
}
