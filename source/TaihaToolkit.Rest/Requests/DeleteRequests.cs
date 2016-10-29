using System.Net;
using System.Net.Http;

namespace Studiotaiha.Toolkit.Rest.Requests
{
	public class DeleteRequest<TParameter, TSuccessResult, TFailureResult> : RestRequestBase<TParameter, TSuccessResult, TFailureResult>
	{
		protected DeleteRequest(string path, string[] acceptContentTypes) : base(HttpMethod.Delete, path, acceptContentTypes)
		{
		}

		protected DeleteRequest(string path) : base(HttpMethod.Delete, path)
		{
		}
	}

	public class NoParamDeleteRequest<TSuccessResult, TFailureResult> : DeleteRequest<object, TSuccessResult, TFailureResult>
	{
		protected NoParamDeleteRequest(string path) : base(path)
		{
		}

		protected NoParamDeleteRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}
	}

	public class NoFailureResultDeleteRequest<TParameter, TSuccessResult> : DeleteRequest<TParameter, TSuccessResult, object>
	{
		protected NoFailureResultDeleteRequest(string path) : base(path)
		{
		}

		protected NoFailureResultDeleteRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}

		protected override bool IsParsableForFailure(HttpStatusCode statusCode, IRequestResult requestResult)
		{
			return false;
		}
	}

	public class NoParamNoFailureResultDeleteRequest<TSuccessResult> : NoParamDeleteRequest<TSuccessResult, object>
	{
		protected NoParamNoFailureResultDeleteRequest(string path) : base(path)
		{
		}

		protected NoParamNoFailureResultDeleteRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}

		protected sealed override bool IsParsableForFailure(HttpStatusCode statusCode, IRequestResult requestResult)
		{
			return false;
		}
	}

	public class NoParamNoSuccessResultDeleteRequest<TFailureResult> : NoParamDeleteRequest<object, TFailureResult>
	{
		protected NoParamNoSuccessResultDeleteRequest(string path) : base(path)
		{
		}

		protected NoParamNoSuccessResultDeleteRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}

		protected sealed override bool IsParsableForSuccess(HttpStatusCode statusCode, IRequestResult requestResult)
		{
			return false;
		}
	}

	public class NoResultDeleteRequest<TParameter> : DeleteRequest<TParameter, object, object>
	{
		protected NoResultDeleteRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}

		protected NoResultDeleteRequest(string path) : base(path)
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

	public class NoResultNoParamDeleteRequest : DeleteRequest<object, object, object>
	{
		protected NoResultNoParamDeleteRequest(string path, string[] acceptContentTypes) : base(path, acceptContentTypes)
		{
		}

		protected NoResultNoParamDeleteRequest(string path) : base(path)
		{
		}
	}
}
