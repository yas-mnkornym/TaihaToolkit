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
		public static void Initialize(params IComponent[] components)
		{
			foreach (var component in components) {
				RegisterComponent(component);
			}
		}

		public static void RegisterComponent(IComponent component)
		{
			Instance.RegisterComponentInternal(component);
		}
		#endregion // Static members


		object ComponentsLock { get; } = new object();
		List<IComponent> ComponentList { get; } = new List<IComponent>();

		private TaihaToolkit()
		{
			RegisterComponentInternal(CoreComponent.Instance);
		}

		internal void RegisterComponentInternal(IComponent component)
		{
			lock (ComponentsLock) {
				if (!ComponentList.Any(x => x.Id == component.Id)) {
					ComponentList.Add(component);
				}
			}
		}
		
		public IEnumerable<IComponent> Components => ComponentList;
	}
}
