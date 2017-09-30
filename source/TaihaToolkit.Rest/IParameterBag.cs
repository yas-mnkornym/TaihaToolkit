﻿using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Studiotaiha.Toolkit.Rest
{
	public interface IParameterBag
	{
		IDictionary<string, string> Query { get; }
		IDictionary<string, string> Body { get; }
		IDictionary<string, string> MultiPartHeader { get; }

		ERequestBodyType RequestBodyType { get; set; }

		/// <summary>
		/// Only referenced when RequestBodyType is RawText
		/// </summary>
		Encoding RawTextEncoding { get; set; }

		/// <summary>
		/// Only referenced when RequestBodyTep is not ApplicationXWwwFormUrlEncoded nor MultiPartFormData
		/// </summary>
		string BodyMediaType { get; set; }

		void AddFilePart(
			string name,
			Stream stream,
			IEnumerable<KeyValuePair<string, string>> properties = null);

		void AddFilePart(
			string name,
			Stream stream,
			int bufferSize,
			IEnumerable<KeyValuePair<string, string>> properties = null);

		void AddTextPart(
			string value,
			Encoding encoding = null,
			IEnumerable<KeyValuePair<string, string>> properties = null);

		void SetStream(
			Stream stream,
			int bufferSize = -1);

		void SetText(
			string text,
			Encoding encoding = null,
			string contentType = null);
	}
}
