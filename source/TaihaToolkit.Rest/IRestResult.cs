namespace Studiotaiha.Toolkit.Rest
{
	public interface IRestResult<TSuccessResult, TFailureResult>
	{
		bool Succeeded { get; }
		TSuccessResult SuccessResult { get; }
		TFailureResult FailureResult { get; }
	}
}
