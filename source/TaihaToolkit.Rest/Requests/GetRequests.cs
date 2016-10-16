using System.Net;
using System.Net.Http;

namespace Studiotaiha.Toolkit.Rest.Requests
{
	public class GetRequest<TParameter, TSuccessResult, TFailureResult> : RestRequestBase<TParameter, TSuccessResult, TFailureResult>
	{
		protected GetRequest(string path, string[] acceptContentTypes) : base(HttpMethod.Get, path, acceptContentTypes)
		{
		}

		protected GetRequest(string path) : base(HttpMethod.Get, path)
		{
		}
	}

	public class NoParamGetRequest<TSuccessResult, TFailureResult> : GetRequest<object, TSuccessResult, TFailureResult>
	{
		protected NoParamGetRequest(string path) : base(path)
		{
		}

		protected NoParamGetRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}
	}

	public class NoFaiulreResultGetRequest<TParameter, TSuccessResult> : GetRequest<TParameter, TSuccessResult, object>
	{
		protected NoFaiulreResultGetRequest(string path) : base(path)
		{
		}

		protected NoFaiulreResultGetRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}

		protected override bool IsParsableForFailure(HttpStatusCode statusCode, IRequestResult requestResult)
		{
			return false;
		}
	}

	public class NoParamNoFailureResultGetRequest<TSuccessResult> : NoParamGetRequest<TSuccessResult, object>
	{
		protected NoParamNoFailureResultGetRequest(string path) : base(path)
		{
		}

		protected NoParamNoFailureResultGetRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}

		protected sealed override bool IsParsableForFailure(HttpStatusCode statusCode, IRequestResult requestResult)
		{
			return false;
		}
	}

	public class NoParamNoSuccessResultGetRequest<TFailureResult> : NoParamGetRequest<object, TFailureResult>
	{
		protected NoParamNoSuccessResultGetRequest(string path) : base(path)
		{
		}

		protected NoParamNoSuccessResultGetRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}

		protected sealed override bool IsParsableForSuccess(HttpStatusCode statusCode, IRequestResult requestResult)
		{
			return false;
		}
	}

	public class NoResultGetRequest<TParameter> : GetRequest<TParameter, object, object>
	{
		protected NoResultGetRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}

		protected NoResultGetRequest(string path) : base(path)
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

	public class NoResultNoParamGetRequest : GetRequest<object, object, object>
	{
		protected NoResultNoParamGetRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}

		protected NoResultNoParamGetRequest(string path) : base(path)
		{
		}
	}
}
