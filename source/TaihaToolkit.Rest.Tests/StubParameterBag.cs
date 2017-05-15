using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Toolkit.Rest.Tests
{
	class StubParameterBag : IParameterBag
	{
		public IDictionary<string, string> Body { get; } = new Dictionary<string, string>();
		public IDictionary<string, string> MultiPartHeader { get; } = new Dictionary<string, string>();
		public IDictionary<string, string> Query { get; } = new Dictionary<string, string>();

		public List<FilePart> FileParts { get; } = new List<FilePart>();
		public List<TextPart> TextParts { get; } = new List<TextPart>();

		public Stream Stream { get; set; }
		public int BufferSize { get; set; }
		public string Text { get; set; }
		public Encoding Encoding { get; set; }
		public ERequestBodyType RequestBodyType { get; set; }
		public Encoding RawTextEncoding { get; set; }

		public void AddFilePart(string name, Stream stream, IEnumerable<KeyValuePair<string, string>> properties = null)
		{
			FileParts.Add(new FilePart {
				Name = name,
				Stream = stream,
				Properties = properties,
			});
		}

		public void AddFilePart(string name, Stream stream, int bufferSize, IEnumerable<KeyValuePair<string, string>> properties = null)
		{
			FileParts.Add(new FilePart {
				Name = name,
				Stream = stream,
				BufferSize = bufferSize,
				Properties = properties,
			});
		}

		public void AddTextPart(string value, Encoding encoding = null, IEnumerable<KeyValuePair<string, string>> properties = null)
		{
			TextParts.Add(new TextPart {
				Text = value,
				Encoding = encoding,
				Properties = properties
			});
		}

		public void SetStream(Stream stream, int bufferSize = -1)
		{
			Stream = stream;
			BufferSize = bufferSize;
		}

		public void SetText(string text, Encoding encoding = null)
		{
			Text = text;
			Encoding = encoding;
		}

		public class FilePart
		{
			public string Name { get; set; }
			public Stream Stream { get; set; }
			public int? BufferSize { get; set; }
			public IEnumerable<KeyValuePair<string, string>> Properties { get; set; }
		}

		public class TextPart
		{
			public string Text { get; set; }
			public Encoding Encoding { get; set; }
			public IEnumerable<KeyValuePair<string, string>> Properties { get; set; }
		}
	}
}
