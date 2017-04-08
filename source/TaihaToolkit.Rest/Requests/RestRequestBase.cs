using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Studiotaiha.Toolkit.Rest.ResultParsers;

namespace Studiotaiha.Toolkit.Rest.Requests
{
	public abstract class RestRequestBase<TParameter, TSuccessResult, TFailureResult> : IRestRequest<TParameter, TSuccessResult, TFailureResult>
	{
		protected RestRequestBase(
			HttpMethod method,
			string path,
			string[] acceptContentTypes)
		{
			Method = method;
			Path = path;
			AcceptContentTypes = acceptContentTypes;
		}

		protected RestRequestBase(
			HttpMethod method,
			string path)
			: this(method, path, new string[] { PreDefinedAcceptContentTypes.Json })
		{
		}

		public string[] AcceptContentTypes { get; }
		public HttpMethod Method { get; }
		public string Path { get; }

		public virtual Task ConfigureHeaderAsync(IHeaderBag headerBag, TParameter parameter)
		{
			return Task.FromResult(0);
		}

		public virtual Task ConfigureParameterAsync(IParameterBag parameterBag, TParameter parameter)
		{
			return Task.FromResult(0);
		}

		public virtual async Task<TFailureResult> ParseFailureResultAsync(HttpStatusCode statusCode, IRequestResult requestResult)
		{
			if (IsParsableForFailure(statusCode, requestResult)) {
				var parser = CreateParser(false);
				if (parser != null) {
					var result = await parser.ParseAsync<TFailureResult>(requestResult);
					return await PostProcessFailureResultAsync(result, statusCode, requestResult);
				}
			}
			throw new RestRequestFailureException(statusCode, await requestResult.ReadAsStringAsync());
		}

		public virtual async Task<TSuccessResult> ParseSuccessResultAsync(HttpStatusCode statusCode, IRequestResult requestResult)
		{
			if (IsParsableForSuccess(statusCode, requestResult)) {
				var parser = CreateParser(true);
				if (parser != null) {
					var result = await parser.ParseAsync<TSuccessResult>(requestResult);
					return await PostProcessSuccessResultAsync(result, statusCode, requestResult);
				}
			}

			return default(TSuccessResult);
		}

		protected virtual Task<TSuccessResult> PostProcessSuccessResultAsync(TSuccessResult result, HttpStatusCode httpStatusCode, IRequestResult requestResult)
		{
			return Task.FromResult(result);
		}

		protected virtual Task<TFailureResult> PostProcessFailureResultAsync(TFailureResult result, HttpStatusCode httpStatusCode, IRequestResult requestResult)
		{
			return Task.FromResult(result);
		}

		protected virtual bool IsParsableForFailure(HttpStatusCode statusCode, IRequestResult requestResult)
		{
			return false;
		}

		protected virtual bool IsParsableForSuccess(HttpStatusCode statusCode, IRequestResult requestResult)
		{
			return true;
		}

		IRequestResultParser CreateParser(bool forSuccess)
		{
			var parserType = this.GetType().GetTypeInfo()
				.GetCustomAttributes<ResultParserAttribute>()
				.Where(x => (forSuccess && x.ForSucccess) || (!forSuccess && x.ForFailure))
				.Where(x => {
					var typeInfo = x.ParserType.GetTypeInfo();
					return typeInfo.IsClass && !typeInfo.IsAbstract;
				})
				.FirstOrDefault()
				?.ParserType;

			if (parserType != null) {
				return (IRequestResultParser)Activator.CreateInstance(parserType);
			}
			else {
				return new DataContractJsonSerializerResultParser();
			}
		}

		public virtual Task<bool> IsSuccessResultAsync(HttpStatusCode statusCode, bool isSuccessStatusCode, IRequestResult requestResult)
		{
			return Task.FromResult(isSuccessStatusCode);
		}
	}
}
