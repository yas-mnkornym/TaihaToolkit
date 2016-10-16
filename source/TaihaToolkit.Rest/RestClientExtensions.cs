using System.Threading;
using System.Threading.Tasks;

namespace Studiotaiha.Toolkit.Rest
{
	public static class RestClientExtensions
	{
		public static Task<IRestResult<TSuccessResult, TFailureResult>> RequestAsync<TSuccessResult, TFailureResult>(
			this IRestClient client,
			IRestRequest<object, TSuccessResult, TFailureResult> request,
			CancellationToken cancellationToken = default(CancellationToken)
			)
		{
			return client.RequestAsync(request, null, cancellationToken);
		}
	}
}
