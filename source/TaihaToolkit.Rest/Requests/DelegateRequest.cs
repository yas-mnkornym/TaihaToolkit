using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Studiotaiha.Toolkit.Rest.Requests
{
	public class DelegateRequest<TParameter, TSuccessResult, TFailureResult> : IRestRequest<TParameter, TSuccessResult, TFailureResult>
	{
		public string[] AcceptContentTypes { get; }
		public HttpMethod Method { get; }
		public string Path { get; }

		public Func<IHeaderBag, TParameter, Task> ConfigureHeaderAsyncHandler { get; set; }
		public Func<IParameterBag, TParameter, Task> ConfigureParameterAsyncHandler { get; set; }
		public Func<HttpStatusCode, IRequestResult, Task<TFailureResult>> ParseFailureResultAsyncHandler { get; set; }
		public Func<HttpStatusCode, IRequestResult, Task<TSuccessResult>> ParseSuccessResultAsyncHandler { get; set; }
		public Func<HttpStatusCode, bool, IRequestResult, Task<bool>> IsSuccessResultAsyncHandler { get; set; }

		public DelegateRequest(
			HttpMethod method,
			string path,
			string[] acceptContentType = null)
		{
			Method = method;
			Path = path;
			AcceptContentTypes = acceptContentType ?? new string[] { PreDefinedAcceptContentTypes.Json };
		}

		public async Task ConfigureHeaderAsync(IHeaderBag headerBag, TParameter parameter)
		{
			if (ConfigureHeaderAsyncHandler != null) {
				await ConfigureHeaderAsyncHandler(headerBag, parameter);
			}
		}

		public async Task ConfigureParameterAsync(IParameterBag parameterBag, TParameter parameter)
		{
			if (ConfigureParameterAsyncHandler != null) {
				await ConfigureParameterAsyncHandler(parameterBag, parameter);
			}
		}

		public async Task<TFailureResult> ParseFailureResultAsync(HttpStatusCode statusCode, IRequestResult requestResult)
		{
			if (ConfigureParameterAsyncHandler != null) {
				return await ParseFailureResultAsyncHandler(statusCode, requestResult);
			}
			else {
				throw new RestRequestFailureException(statusCode, await requestResult.ReadAsStringAsync());
			}
		}

		public async Task<TSuccessResult> ParseSuccessResultAsync(HttpStatusCode statusCode, IRequestResult requestResult)
		{
			if (ParseSuccessResultAsyncHandler != null) {
				return await ParseSuccessResultAsyncHandler(statusCode, requestResult);
			}
			else {
				return default(TSuccessResult);
			}
		}

		public async Task<bool> IsSuccessResultAsync(HttpStatusCode statusCode, bool isSuccessStatusCode, IRequestResult requestResult)
		{
			if (IsSuccessResultAsyncHandler != null) {
				return await IsSuccessResultAsyncHandler(statusCode, isSuccessStatusCode, requestResult);
			}
			else {
				return isSuccessStatusCode;
			}
		}
	}
}