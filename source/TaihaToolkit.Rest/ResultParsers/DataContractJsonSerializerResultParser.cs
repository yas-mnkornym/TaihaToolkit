using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace Studiotaiha.Toolkit.Rest.ResultParsers
{
	public class DataContractJsonSerializerResultParser : IRequestResultParser
	{
		public Type[] KnownTypes { get; }

		public DataContractJsonSerializerResultParser()
		{
			KnownTypes = new Type[] { };
		}

		public DataContractJsonSerializerResultParser(IEnumerable<Type> knownTypes)
		{
			KnownTypes = knownTypes.ToArray();
		}

		public async Task<TResult> ParseAsync<TResult>(IRequestResult result)
		{
			using (var stream = await result.ReadAsStreamAsync()) {
				var serializer = KnownTypes?.Length > 0
					? new DataContractJsonSerializer(typeof(TResult), KnownTypes)
					: new DataContractJsonSerializer(typeof(TResult));
				return (TResult)serializer.ReadObject(stream);
			}
		}
	}
}
