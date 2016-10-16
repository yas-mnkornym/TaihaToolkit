using System.Threading.Tasks;

namespace Studiotaiha.Toolkit.Rest
{
	public interface IRequestResultParser
	{
		Task<TResult> ParseAsync<TResult>(IRequestResult result);
	}
}
