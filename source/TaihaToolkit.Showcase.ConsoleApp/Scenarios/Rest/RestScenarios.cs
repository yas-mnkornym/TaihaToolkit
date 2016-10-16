using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StudioTaiha.Toolkit.Showcase.ConsoleApp.Scenarios.Rest
{
    [RootScenario]
    class RestScenario : IScenario
    {
        public string Description { get; } = "Show samples of Rest client.";

        public bool IsExecutable { get; } = false;

        public string Title { get; } = "Rest";

        public void Execute()
        {
            throw new NotSupportedException();
        }

        public IEnumerable<IScenario> SubScenarios { get; } =
            Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.IsSubclassOf(typeof(RestScenarioBase)))
                .Select(type => (IScenario)Activator.CreateInstance(type));
    }
}
