using System.Net;
using System.Net.Http;

namespace Studiotaiha.Toolkit.Rest.Requests
{
	public class PostRequest<TParameter, TSuccessResult, TFailureResult> : RestRequestBase<TParameter, TSuccessResult, TFailureResult>
	{
		protected PostRequest(string path, string[] acceptContentTypes) : base(HttpMethod.Post, path, acceptContentTypes)
		{
		}

		protected PostRequest(string path) : base(HttpMethod.Post, path)
		{
		}
	}

	public class NoFaiulreResultPostRequest<TParameter, TSuccessResult> : PostRequest<TParameter, TSuccessResult, object>
	{
		protected NoFaiulreResultPostRequest(string path) : base(path)
		{
		}

		protected NoFaiulreResultPostRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}

		protected override bool IsParsableForFailure(HttpStatusCode statusCode, IRequestResult requestResult)
		{
			return false;
		}
	}

	public class NoParamPostRequest<TSuccessResult, TFailureResult> : PostRequest<object, TSuccessResult, TFailureResult>
	{
		protected NoParamPostRequest(string path) : base(path)
		{
		}

		protected NoParamPostRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}
	}

	public class NoParamNoFailureResultPostRequest<TSuccessResult> : NoParamPostRequest<TSuccessResult, object>
	{
		protected NoParamNoFailureResultPostRequest(string path) : base(path)
		{
		}

		protected NoParamNoFailureResultPostRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}

		protected sealed override bool IsParsableForFailure(HttpStatusCode statusCode, IRequestResult requestResult)
		{
			return false;
		}
	}

	public class NoParamNoSuccessResultPostRequest<TFailureResult> : NoParamPostRequest<object, TFailureResult>
	{
		protected NoParamNoSuccessResultPostRequest(string path) : base(path)
		{
		}

		protected NoParamNoSuccessResultPostRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}

		protected sealed override bool IsParsableForSuccess(HttpStatusCode statusCode, IRequestResult requestResult)
		{
			return false;
		}
	}

	public class NoResultPostRequest<TParameter> : PostRequest<TParameter, object, object>
	{
		protected NoResultPostRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}

		protected NoResultPostRequest(string path) : base(path)
		{
		}

		protected sealed override bool IsParsableForFailure(HttpStatusCode statusCode, IRequestResult requestResult)
		{
			return false;
		}

		protected sealed override bool IsParsableForSuccess(HttpStatusCode statusCode, IRequestResult requestResult)
		{
			return false;
		}
	}

	public class NoResultNoParamPostRequest : PostRequest<object, object, object>
	{
		protected NoResultNoParamPostRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}

		protected NoResultNoParamPostRequest(string path) : base(path)
		{
		}
	}
}
