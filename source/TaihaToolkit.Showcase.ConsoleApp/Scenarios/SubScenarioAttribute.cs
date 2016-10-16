using System;

namespace StudioTaiha.Toolkit.Showcase.ConsoleApp.Scenarios
{
	[System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	sealed class SubScenarioAttribute : Attribute
	{
		public SubScenarioAttribute(Type parentScenarioType)
		{
			ParentScenarioType = parentScenarioType;
		}

		public Type ParentScenarioType { get; }
	}
}
