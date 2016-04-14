using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyInfoCmdlet
{
	public class AssemblyInfoDeserializer<TAssemblyInfo>
		where TAssemblyInfo : class, new()
	{
		public TAssemblyInfo Deserialize(IEnumerable<Property> assemblyInfoProperties)
		{
			var info = new TAssemblyInfo();
			var properties = info.GetType()
				.GetProperties()
				.Where(x => x.CanWrite);
			foreach (var property in properties) {
				var name = property.Name;
				var attr = property.GetCustomAttributes(typeof(AssemblyInfoPropertyAttribute), true).FirstOrDefault() as AssemblyInfoPropertyAttribute;
				var propName = attr?.PropertyName ?? name;

				var prop = assemblyInfoProperties.FirstOrDefault(x => x.Name == propName);
				if (prop != null) {
					AssignValue(info, property, prop);
				}
			}
			return info;
		}

		protected void AssignValue(
			object targetObject,
			PropertyInfo targetProperty,
			Property property)
		{
			var propType = targetProperty.PropertyType;

			var propValue = property.Values[0];
			object value = null;
			if (propType == typeof(int)) {
				value = Convert.ToInt32(propValue.Value);
			}
			else if (propType == typeof(long)) {
				value = Convert.ToInt64(propValue.Value);
			}
			else if (propType == typeof(bool)) {
				value = Convert.ToBoolean(propValue.Value);
			}
			else if (propType == typeof(string)) {
				value = Convert.ToString(propValue.Value);
			}
			else if (propType == typeof(Guid)) {
				value = Guid.Parse(propValue.Value);
			}
			else {
				throw new NotImplementedException();
			}

			targetProperty.SetValue(targetObject, value);
		}
	}
}
