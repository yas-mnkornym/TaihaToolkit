using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Studiotaiha.Toolkit.Rest
{
	public interface IRestRequest
	{
		HttpMethod Method { get; }
		string Path { get; }
		string[] AcceptContentTypes { get; }
		Task<bool> IsSuccessResultAsync(HttpStatusCode statusCode, bool isSuccessStatusCode, IRequestResult requestResult);
	}

	public interface IRestRequest<TParameter, TSuccessResult, TFailureResult> : IRestRequest
	{
		Task ConfigureHeaderAsync(IHeaderBag headerBag, TParameter parameter);
		Task ConfigureParameterAsync(IParameterBag parameterBag, TParameter parameter);

		Task<TSuccessResult> ParseSuccessResultAsync(HttpStatusCode statusCode, IRequestResult requestResult);
		Task<TFailureResult> ParseFailureResultAsync(HttpStatusCode statusCode, IRequestResult requestResult);
	}
}
