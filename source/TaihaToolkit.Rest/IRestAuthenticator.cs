using System.Threading.Tasks;

namespace Studiotaiha.Toolkit.Rest
{
	public interface IRestAuthenticator
	{
		Task AuthenticateAsync(
			IRestClient client,
			IRestRequest request,
			IHeaderBag headerBag,
			IParameterBag parameterBag);
	}
}
