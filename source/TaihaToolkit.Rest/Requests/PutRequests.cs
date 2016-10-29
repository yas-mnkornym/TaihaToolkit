using System.Net;
using System.Net.Http;

namespace Studiotaiha.Toolkit.Rest.Requests
{
	public class PutRequest<TParameter, TSuccessResult, TFailureResult> : RestRequestBase<TParameter, TSuccessResult, TFailureResult>
	{
		protected PutRequest(string path, string[] acceptContentTypes) : base(HttpMethod.Put, path, acceptContentTypes)
		{
		}

		protected PutRequest(string path) : base(HttpMethod.Put, path)
		{
		}
	}

	public class NoParamPutRequest<TSuccessResult, TFailureResult> : PutRequest<object, TSuccessResult, TFailureResult>
	{
		protected NoParamPutRequest(string path) : base(path)
		{
		}

		protected NoParamPutRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}
	}

	public class NoFailureResultPutRequest<TParameter, TSuccessResult> : PutRequest<TParameter, TSuccessResult, object>
	{
		protected NoFailureResultPutRequest(string path) : base(path)
		{
		}

		protected NoFailureResultPutRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}

		protected override bool IsParsableForFailure(HttpStatusCode statusCode, IRequestResult requestResult)
		{
			return false;
		}
	}

	public class NoParamNoFailureResultPutRequest<TSuccessResult> : NoParamPutRequest<TSuccessResult, object>
	{
		protected NoParamNoFailureResultPutRequest(string path) : base(path)
		{
		}

		protected NoParamNoFailureResultPutRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}

		protected sealed override bool IsParsableForFailure(HttpStatusCode statusCode, IRequestResult requestResult)
		{
			return false;
		}
	}

	public class NoParamNoSuccessResultPutRequest<TFailureResult> : NoParamPutRequest<object, TFailureResult>
	{
		protected NoParamNoSuccessResultPutRequest(string path) : base(path)
		{
		}

		protected NoParamNoSuccessResultPutRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}

		protected sealed override bool IsParsableForSuccess(HttpStatusCode statusCode, IRequestResult requestResult)
		{
			return false;
		}
	}

	public class NoResultPutRequest<TParameter> : PutRequest<TParameter, object, object>
	{
		protected NoResultPutRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}

		protected NoResultPutRequest(string path) : base(path)
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

	public class NoResultNoParamPutRequest : PutRequest<object, object, object>
	{
		protected NoResultNoParamPutRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}

		protected NoResultNoParamPutRequest(string path) : base(path)
		{
		}
	}
}
