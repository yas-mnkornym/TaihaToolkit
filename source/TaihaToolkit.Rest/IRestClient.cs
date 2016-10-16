using System.Threading;
using System.Threading.Tasks;

namespace Studiotaiha.Toolkit.Rest
{
	public interface IRestClient
	{
		Task<IRestResult<TSuccessResult, TFailureResult>> RequestAsync<TParameter, TSuccessResult, TFailureResult>(
			IRestRequest<TParameter, TSuccessResult, TFailureResult> request,
			TParameter parameter,
			CancellationToken cancellationToken = default(CancellationToken)
			);
	}
}
