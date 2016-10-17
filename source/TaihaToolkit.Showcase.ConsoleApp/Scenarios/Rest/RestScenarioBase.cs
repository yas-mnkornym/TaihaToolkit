using System;
using Studiotaiha.Toolkit.Rest;

namespace StudioTaiha.Toolkit.Showcase.ConsoleApp.Scenarios.Rest
{
    abstract class RestScenarioBase : SubScenarioBase
    {
        protected RestScenarioBase(
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
    }
}
