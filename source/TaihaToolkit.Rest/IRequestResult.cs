using System.IO;
using System.Threading.Tasks;

namespace Studiotaiha.Toolkit.Rest
{
	public interface IRequestResult
	{
		Task<string> ReadAsStringAsync();
		Task<Stream> ReadAsStreamAsync();
		Task<byte[]> ReadAsByteArrayAsync();
	}
}
