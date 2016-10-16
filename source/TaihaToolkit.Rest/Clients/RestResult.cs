namespace Studiotaiha.Toolkit.Rest.Clients
{
	class RestResult<TSuccessReuslt, TFailureResult> : IRestResult<TSuccessReuslt, TFailureResult>
	{
		public bool Succeeded { get; set; }
		public TFailureResult FailureResult { get; set; }
		public TSuccessReuslt SuccessResult { get; set; }
	}
}
