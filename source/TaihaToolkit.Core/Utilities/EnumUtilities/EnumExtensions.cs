using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Studiotaiha.Toolkit.Utilities.EnumUtilities
{
	public static class EnumExtensions
	{
		public static IEnumerable<TEnum> SplitFlags<TEnum>(
			this Enum value,
			bool excludeDefaultValue = true)
			where TEnum : struct
		{
			var members = value.GetType().GetTypeInfo().DeclaredMembers;
			return Enum.GetValues(value.GetType())
				.Cast<Enum>()
				.Where(x => value.HasFlag(x))
				.Cast<TEnum>()
				.Where(x => members.FirstOrDefault(y => y.Name == x.ToString())?.GetCustomAttribute<CombinedFlagAttribute>(false) == null)
				.Where(x => !excludeDefaultValue || (excludeDefaultValue && !object.Equals(x, default(TEnum))));
		}

		public static Attribute GetAtribute<TAttribute>(
			this Enum value,
			bool inherit = false)
			where TAttribute : Attribute
		{
			return value.GetMemebrInfo()
				?.GetCustomAttribute<TAttribute>(inherit);
		}

		public static IEnumerable<Attribute> GetAtributes<TAttribute>(
			this Enum value,
			bool inherit = false)
			where TAttribute : Attribute
		{
			return value.GetMemebrInfo()
				?.GetCustomAttributes<TAttribute>(inherit);
		}

		public static bool HasAttribute<TAttribute>(
			this Enum value,
			bool inherit = false)
			where TAttribute : Attribute
		{
			return value.GetAtribute<TAttribute>(inherit) != null;
		}

		public static MemberInfo GetMemebrInfo(this Enum value)
		{
			return value.GetType().GetTypeInfo().DeclaredMembers
				   .FirstOrDefault(x => x.Name == value.ToString());
		}
	}
}
