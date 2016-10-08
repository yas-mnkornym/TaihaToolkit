using System;
using System.Collections.Generic;
using System.Linq;
using Studiotaiha.Toolkit.Composition;

namespace Studiotaiha.Toolkit
{
	public class TaihaToolkit
	{
		#region Singleton

		static TaihaToolkit current_;
		public static TaihaToolkit Current => current_ ?? (current_ = new TaihaToolkit());

		#endregion // Singleton


		#region Static values
		
		public static Guid ServiceIdDialog { get; } = new Guid("F715924A-5BE4-4AD7-A55C-5D65F4C91ACB");
		public static Guid ServiceIdLogging { get; } = new Guid("FAB5D959-4669-4609-95C7-C11ACED408C8");
		public static Guid ServiceIdSettings { get; } = new Guid("D8C079FF-C37B-461F-BF3E-8280733F58E6");

		#endregion
		
		private TaihaToolkit()
		{
			RegisterComponent(CoreComponent.Instance);
		}


		#region Component registration

		List<IComponent> ComponentList { get; } = new List<IComponent>();
		object ComponentsLock { get; } = new object();
		public IEnumerable<IComponent> Components => ComponentList;

		/// <summary>
		/// Register components
		/// </summary>
		/// <param name="components">Components to be registered.</param>
		public void RegisterComponents(params IComponent[] components)
		{
			foreach (var component in components) {
				RegisterComponent(component);
			}
		}

		/// <summary>
		/// Register a component
		/// </summary>
		/// <param name="component">Component to be registered.</param>
		public void RegisterComponent(IComponent component)
		{
			lock (ComponentsLock) {
				if (!ComponentList.Any(x => x.Id == component.Id)) {
					ComponentList.Add(component);
					ComponentRegistered?.Invoke(this, component);
				}
			}
		}

		/// <summary>
		/// An event notifies registration of a component.
		/// </summary>
		public event EventHandler<IComponent> ComponentRegistered;

		#endregion // Component registration


		#region Service overrride

		object ServiceOverrideLock { get; } = new object();
		Dictionary<Guid, IComponent> OverriddenServiceMap { get; } = new Dictionary<Guid, IComponent>();

		/// <summary>
		/// Gets a map of overridden services
		/// </summary>
		public IReadOnlyDictionary<Guid, IComponent> OverriddenServices { get; }

		/// <summary>
		/// Override some service by a component specified.
		/// </summary>
		/// <param name="serviceId">Id of the service to be overridden.</param>
		/// <param name="component">Component</param>
		public void OverrideService(Guid serviceId, IComponent component)
		{
			if (component == null) { throw new ArgumentNullException(nameof(component)); }

			lock (ServiceOverrideLock) {
				IComponent oldValue;
				var isChanged = OverriddenServiceMap.TryGetValue(serviceId, out oldValue)
					? oldValue == component
					: true;

				if (isChanged) {
					OverriddenServiceMap[serviceId] = component;
					ServiceOverrided?.Invoke(this, serviceId);
				}
			}
		}
		
		/// <summary>
		/// Clear a service override of the service specified.
		/// </summary>
		/// <param name="serviceId">Service ID that service override to be cleard.</param>
		public void ClearServiceOverride(Guid serviceId)
		{
			lock (ServiceOverrideLock) {
				if (OverriddenServiceMap.Remove(serviceId)) {
					ServiceOverrideCleard?.Invoke(this, serviceId);
				}
			}
		}

		/// <summary>
		/// Clear all service overrides.
		/// </summary>
		public void ClearServiceOverrides()
		{
			lock (ServiceOverrideLock) {
				var serviceIds = OverriddenServiceMap.Keys;
				OverriddenServiceMap.Clear();
				foreach (var serviceId in serviceIds) {
					ServiceOverrideCleard?.Invoke(this, serviceId);
				}
			}
		}

		/// <summary>
		/// Get component object for a service specified.
		/// </summary>
		/// <param name="serviceId">Service ID</param>
		/// <returns>Overriding component object for the service</returns>
		public IComponent GetOverridingComponent(Guid serviceId)
		{
			lock (ServiceOverrideLock) {
				IComponent result;
				OverriddenServiceMap.TryGetValue(serviceId, out result);
				return result;
			}
		}

		/// <summary>
		/// An event notifies that a service is overrides.
		/// </summary>
		public event EventHandler<Guid> ServiceOverrided;

		/// <summary>
		/// An event notifies that a service override is cleard.
		/// </summary>
		public event EventHandler<Guid> ServiceOverrideCleard;

		#endregion // Service override
	}
}
