using System;
using System.Net;

namespace Studiotaiha.Toolkit.Rest
{
	public class RestException : Exception
	{
		public RestException() { }
		public RestException(string message) : base(message) { }
		public RestException(string message, Exception inner) : base(message, inner) { }
	}
	
	public class RestRequestFailureException : RestException
	{
		public RestRequestFailureException(HttpStatusCode statusCode, string responseBody)
			: base($"Rest request failed with status code: {(int)statusCode} ({statusCode.ToString()})")
		{
			StatusCode = statusCode;
			ResponseBody = responseBody;
		}

		public string ResponseBody { get; }
		public HttpStatusCode StatusCode { get; }
	}
}
