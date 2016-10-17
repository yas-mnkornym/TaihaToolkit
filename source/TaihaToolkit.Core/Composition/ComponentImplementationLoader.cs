using System;
using System.Linq;
using System.Reflection;

namespace Studiotaiha.Toolkit.Composition
{
	public class ComponentImplementationLoader<TInterface>
		where TInterface : class
	{
		Guid? ServiceId { get; }

		public ComponentImplementationLoader()
		{ }

		public ComponentImplementationLoader(Guid serviceId)
		{
			ServiceId = serviceId;
		}

		public TInterface CreateInstance(params object[] args)
		{
			var overridingComponent = ServiceId.HasValue
				? TaihaToolkit.Current.GetOverridingComponent(ServiceId.Value)
				: null;
			
			var interfaceType = typeof(TInterface);

			var typesToCheck = overridingComponent != null
				? overridingComponent.Assembly.DefinedTypes
				: TaihaToolkit.Current.Components.Select(x => x.Assembly)
					.ToArray()
					.SelectMany(x => x.DefinedTypes);

			var targetType = typesToCheck	
				.Where(x => x.IsPublic && x.IsClass && !x.IsAbstract)
				.Where(x => x.ImplementedInterfaces.Contains(interfaceType) || x.IsSubclassOf(interfaceType))
				.Select(x => new { TypeInfo = x, Attributes = x.GetCustomAttributes<ComponentImplementationAttribute>() })
				.SelectMany(x => x.Attributes.Select(attr => new { TypeInfo = x.TypeInfo, Attribute = attr }))
				.Where(x => x.Attribute.InterfaceType == interfaceType)
				.OrderBy(x => x.Attribute.Priority)
				.FirstOrDefault()
				?.TypeInfo
				?.AsType();

			return targetType == null
				? null
				: (TInterface)Activator.CreateInstance(targetType, args);
		}
	}
}
