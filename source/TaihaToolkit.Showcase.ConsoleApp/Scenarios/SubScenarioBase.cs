using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StudioTaiha.Toolkit.Showcase.ConsoleApp.Scenarios
{
	public abstract class SubScenarioBase : IScenario
	{
		public SubScenarioBase()
		{
			var thisType = this.GetType();
			SubScenarios = Assembly.GetExecutingAssembly()
				.GetTypes()
				.Where(x => x.GetInterface(typeof(IScenario).FullName) != null)
				.Select(type => new { Type = type, Attributes = type.GetCustomAttributes(typeof(SubScenarioAttribute), false).Cast<SubScenarioAttribute>() })
				.Where(x => x.Attributes.Any(y => y.ParentScenarioType == thisType))
				.Select(x => (IScenario)Activator.CreateInstance(x.Type));
		}

		public virtual string Description { get; set; }
		public virtual bool IsExecutable { get; set; }

		public virtual IEnumerable<IScenario> SubScenarios { get; }

		public virtual string Title { get; set; }

		public abstract void Execute();
	}
}
