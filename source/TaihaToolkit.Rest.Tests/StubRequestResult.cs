using System.IO;
using System.Threading.Tasks;

namespace Studiotaiha.Toolkit.Rest.Tests
{
	public class StubRequestResult : IRequestResult
	{
		public Task<byte[]> ReadAsByteArrayAsync()
		{
			return Task.FromResult<byte[]>(null);
		}

		public Task<Stream> ReadAsStreamAsync()
		{
			return Task.FromResult<Stream>(null);
		}

		public Task<string> ReadAsStringAsync()
		{
			return Task.FromResult<string>(null);
		}
	}
}
