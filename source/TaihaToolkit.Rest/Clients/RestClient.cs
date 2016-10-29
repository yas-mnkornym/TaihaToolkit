using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Studiotaiha.Toolkit.Rest.Clients
{
	public class RestClient : IRestClient
	{
		public string AuthrizationHeaderKey { get; } = "Authorization";
		public string UserAgentHeaderKey { get; } = "User-Agent";

		public Uri BaseUri { get; }
		public IRestAuthenticator Authenticator { get; set; }
		public string UserAgent { get; set; }

		public RestClient(Uri baseUri)
		{
			if (baseUri == null) { throw new ArgumentNullException(nameof(baseUri)); }
			BaseUri = baseUri;
		}

		public async Task<IRestResult<TSuccessResult, TFailureResult>> RequestAsync<TParameter, TSuccessResult, TFailureResult>(IRestRequest<TParameter, TSuccessResult, TFailureResult> request, TParameter parameter, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (request == null) { throw new ArgumentNullException(nameof(request)); }

			var headerBag = new HeaderBag();
			var parameterBag = new ParameterBag();

			// Authenticate request
			if (Authenticator != null) {
				await Authenticator.AuthenticateAsync(this, request, headerBag, parameterBag);
			}

			// Construct actual request Uri
			var requestUri = ConstructRequestUri(BaseUri, request.Path, parameterBag);

			// Construct request message, then request
			using (HttpClient client = new HttpClient())
			using (var requestMessage = new HttpRequestMessage(request.Method, requestUri)) {
				// Configure header
				await request.ConfigureHeaderAsync(headerBag, parameter);

				bool isUserAgentChanged = false;
				foreach (var kv in headerBag) {
					var key = kv.Key;
					var value = kv.Value;

					if (key == AuthrizationHeaderKey) { // Authorization
						var tokens = value.Split(' ');
						if (tokens.Length == 2) {
							var scheme = tokens[0];
							var token = tokens[1];
							requestMessage.Headers.Authorization = new AuthenticationHeaderValue(scheme, token);
						}
						else {
							requestMessage.Headers.Add(key, value);
						}
					}
					else if (key == UserAgentHeaderKey) { // User-Agent
						client.DefaultRequestHeaders.Add(UserAgentHeaderKey, value);
						isUserAgentChanged = true;
					}
					else { // Other header values
						requestMessage.Headers.Add(key, value);
					}
				}

				// Confiure User-Agent if needed
				if (!isUserAgentChanged && !string.IsNullOrWhiteSpace(UserAgent)) {
					client.DefaultRequestHeaders.Add(UserAgentHeaderKey, UserAgent);
					isUserAgentChanged = true;
				}

				// Configure AcceptContentType
				if (request.AcceptContentTypes != null) {
					foreach (var acceptContentType in request.AcceptContentTypes) {
						requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptContentType));
					}
				}


				// Concigure parameter
				await request.ConfigureParameterAsync(parameterBag, parameter);

				// Configure Body
				if (parameterBag.RequestBodyType == ERequestBodyType.ApplicationXWwwFormUrlEncoded) {
					if (parameterBag.Body.Any()) {
						requestMessage.Content = new FormUrlEncodedContent(parameterBag.Body);
					}
				}
				else if (parameterBag.RequestBodyType == ERequestBodyType.RawBytes) {
					requestMessage.Content = new StreamContent(parameterBag.ContentStream, parameterBag.ContentSize);
				}
				else if (parameterBag.RequestBodyType == ERequestBodyType.RawText) {
					requestMessage.Content = parameterBag.RawTextEncoding == null
						? new StringContent(parameterBag.RawText)
						: new StringContent(parameterBag.RawText, parameterBag.RawTextEncoding);
				}
				else if (parameterBag.RequestBodyType == ERequestBodyType.MultiPartFormData) {
					var multiPartContent = new MultipartFormDataContent();
					foreach (var content in parameterBag.MultiPartContents) {
						multiPartContent.Add(content);
					}
				}
				else {
					throw new NotSupportedException($"Request body type {parameterBag.RequestBodyType} is not supported.");
				}

				// Send request
				using (var response = await client.SendAsync(requestMessage)) {
					var restResult = new RestResult<TSuccessResult, TFailureResult>();

					// Parse response
					var requestResult = new RequestResult(response);
					if (restResult.Succeeded = await request.IsSuccessResultAsync(response.StatusCode, response.IsSuccessStatusCode, requestResult)){
						restResult.SuccessResult = await request.ParseSuccessResultAsync(response.StatusCode, requestResult);
					}
					else {
						restResult.FailureResult = await request.ParseFailureResultAsync(response.StatusCode, requestResult);
					}

					return restResult;
				}
			}
		}

		static Uri ConstructRequestUri(Uri baseUri, string path, ParameterBag parameterBag)
		{
			if (string.IsNullOrWhiteSpace(path)) {
				return baseUri;
			}

			var urlBuilder = new UriBuilder(baseUri);
			var basePath = urlBuilder.Path.TrimEnd('/');
			var requestPath = path.TrimStart('/');
			urlBuilder.Path = basePath + "/" + requestPath;

			if (parameterBag.Query.Any()) {
				urlBuilder.Query = string.Join("&",
					parameterBag.Query.Select(x => {
						var key = WebUtility.UrlEncode(x.Key);
						var value = WebUtility.UrlEncode(x.Value);
						return key + "=" + value;
					}));
			}

			return urlBuilder.Uri;
		}
	}
}
