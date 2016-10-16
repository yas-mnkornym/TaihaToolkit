using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Studiotaiha.Toolkit.Rest
{
	public static class ParameterBagExtensions
	{
		public static void AddFilePart(
			this IParameterBag parameterBag,
			string name,
			string fileName,
			Stream stream,
			IEnumerable<KeyValuePair<string, string>> properties = null)
		{
			parameterBag.AddFilePart(name, stream, new {
				filename = fileName,
			});
		}

		public static void AddFilePart<T>(
			this IParameterBag parameterBag,
			string name,
			Stream stream,
			T properties)
			where T : class
		{
			parameterBag.AddFilePart(name, stream, ParseProperties(properties));
		}
		public static void AddFilePart(
			this IParameterBag parameterBag,
			string name,
			string fileName,
			Stream stream,
			int bufferSize,
			IEnumerable<KeyValuePair<string, string>> properties = null)
		{
			parameterBag.AddFilePart(name, stream, bufferSize, new {
				filename = fileName,
			});
		}

		public static void AddFilePart<T>(
			this IParameterBag parameterBag,
			string name,
			Stream stream,
			int bufferSize,
			T properties)
			where T : class
		{
			parameterBag.AddFilePart(name, stream, bufferSize, ParseProperties(properties));
		}

		public static void AddTextPart<T>(
			this IParameterBag parameterBag,
			string value,
			Encoding encoding,
			T properties)
			where T : class
		{
			parameterBag.AddTextPart(value, encoding, ParseProperties(properties));
		}

		public static void AddTextPart<T>(
			this IParameterBag parameterBag,
			string value,
			T properties)
			where T : class
		{
			parameterBag.AddTextPart(value, null, properties);
		}

		static IEnumerable<KeyValuePair<string, string>> ParseProperties<T>(T obj)
			where T : class
		{
			return obj.GetType().GetTypeInfo().DeclaredProperties
				.Select(x => new KeyValuePair<string, string>(x.Name, x.GetValue(obj)?.ToString()));
		}
	}
}
