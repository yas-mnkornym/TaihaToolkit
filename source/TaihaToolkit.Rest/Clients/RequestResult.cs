using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Studiotaiha.Toolkit.Rest.Clients
{
	class RequestResult : IRequestResult
	{
		HttpResponseMessage Response { get; }

		public RequestResult(HttpResponseMessage response)
		{
			if (response == null) { throw new ArgumentNullException(nameof(response)); }
			Response = response;
		}

		public async Task<string> ReadAsStringAsync()
		{
			return await Response.Content.ReadAsStringAsync();
		}

		public async Task<Stream> ReadAsStreamAsync()
		{
			return await Response.Content.ReadAsStreamAsync();
		}

		public async Task<byte[]> ReadAsByteArrayAsync()
		{
			return await Response.Content.ReadAsByteArrayAsync();
		}
	}
}
