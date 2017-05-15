using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace Studiotaiha.Toolkit.Rest.Clients
{
	class ParameterBag : IParameterBag
	{
		public IDictionary<string, string> Body { get; } = new Dictionary<string, string>();
		public IDictionary<string, string> Query { get; } = new Dictionary<string, string>();
		public IDictionary<string, string> MultiPartHeader { get; } = new Dictionary<string, string>();
		public ERequestBodyType RequestBodyType { get; set; } = ERequestBodyType.ApplicationXWwwFormUrlEncoded;
		public List<HttpContent> MultiPartContents { get; } = new List<HttpContent>();

		public Stream ContentStream { get; private set; }
		public int ContentSize { get; private set; }
		public string RawText { get; set; }
		public Encoding RawTextEncoding { get; set; }


		public void SetStream(Stream stream, int size = -1)
		{
			ContentStream = stream;
			ContentSize = size;
		}

		public void AddFilePart(string name, Stream stream, IEnumerable<KeyValuePair<string, string>> properties = null)
		{
			AddFilePart(name, stream, -1, properties);
		}

		public void AddFilePart(string name, Stream stream, int bufferSize, IEnumerable<KeyValuePair<string, string>> properties = null)
		{
			var content = bufferSize == -1
				? new StreamContent(stream)
				: new StreamContent(stream, bufferSize);
			foreach (var property in properties) {
				content.Headers.Add(property.Key, property.Value);
			}
			MultiPartContents.Add(content);
		}

		public void AddTextPart(string value, Encoding encoding = null, IEnumerable<KeyValuePair<string, string>> properties = null)
		{
			var content = encoding == null
				? new StringContent(value)
				: new StringContent(value, encoding);
			foreach (var property in properties) {
				content.Headers.Add(property.Key, property.Value);
			}
			MultiPartContents.Add(content);
		}

		public void SetText(string text, Encoding encoding = null)
		{
			RawText = text ?? throw new ArgumentNullException(nameof(text));
			RawTextEncoding = encoding;
		}
	}
}
