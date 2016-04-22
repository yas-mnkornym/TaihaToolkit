using System;
using System.Runtime.Serialization;

namespace Studiotaiha.Toolkit.Core.Tests.Settings
{

	[DataContract]
	class SettingsTestClass : IEquatable<SettingsTestClass>
	{
		[DataMember]
		public int Int { get; set; }

		[DataMember]
		public string String { get; set; }

		[DataMember]
		public float Float { get; set; }

		[DataMember]
		public SettingsTestClass Test { get; set; }

		public bool Equals(SettingsTestClass other)
		{
			return (
				(Test == null && other.Test == null ||
				Test?.Equals(other?.Test) == false) &&
				Int == other?.Int &&
				String == other?.String &&
				Float == other?.Float
			);
		}

		public override bool Equals(object obj)
		{
			if (obj is SettingsTestClass) {
				return Equals((SettingsTestClass)obj);
			}

			return false;
		}

		public static bool operator ==(SettingsTestClass lhs, SettingsTestClass rhs)
		{
			return lhs?.Equals(rhs) == true;
		}

		public static bool operator !=(SettingsTestClass lhs, SettingsTestClass rhs)
		{
			return !(lhs == rhs);
		}

		public override int GetHashCode()
		{
			return (
				Test?.GetHashCode() ?? 0 ^
				Int.GetHashCode() ^
				String?.GetHashCode() ?? 0 ^
				Float.GetHashCode());
		}
	}
}
