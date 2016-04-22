using System;
using System.Collections.Generic;
using System.Linq;
using Studiotaiha.Toolkit.Composition;

namespace Studiotaiha.Toolkit
{
	public class TaihaToolkit
	{
		#region Singleton

		static TaihaToolkit instance_;
		internal static TaihaToolkit Instance => instance_ ?? (instance_ = new TaihaToolkit());

		#endregion // Singleton

		#region Static members

		/// <summary>
		/// Register components
		/// </summary>
		/// <param name="components">Components to be registered.</param>
		public static void RegisterComponents(params IComponent[] components)
		{
			foreach (var component in components) {
				RegisterComponent(component);
			}
		}

		/// <summary>
		/// Register a component
		/// </summary>
		/// <param name="component">Component to be registered.</param>
		public static void RegisterComponent(IComponent component)
		{
			Instance.RegisterComponentInternal(component);
		}

		#endregion // Static members


		object ComponentsLock { get; } = new object();

		List<IComponent> ComponentList { get; } = new List<IComponent>();

		public IEnumerable<IComponent> Components => ComponentList;

		private TaihaToolkit()
		{
			RegisterComponentInternal(CoreComponent.Instance);
		}

		internal void RegisterComponentInternal(IComponent component)
		{
			lock (ComponentsLock) {
				if (!ComponentList.Any(x => x.Id == component.Id)) {
					ComponentList.Add(component);
					ComponentRegistered?.Invoke(this, component);
				}
			}
		}

		public event EventHandler<IComponent> ComponentRegistered;
	}
}
