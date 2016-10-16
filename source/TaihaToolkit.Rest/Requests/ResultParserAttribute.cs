using System;

namespace Studiotaiha.Toolkit.Rest.Requests
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class ResultParserAttribute : Attribute
	{
		public ResultParserAttribute(
			Type parserType,
			bool forSuccess,
			bool forFailure)
		{
			ParserType = parserType;
			ForSucccess = forSuccess;
			ForFailure = forFailure;
		}

		public Type ParserType { get; }
		public bool ForSucccess { get; }
		public bool ForFailure { get; }
	}
}
