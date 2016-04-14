using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AssemblyInfoCmdlet
{
	internal class AssemblyInfoParser
	{
		static string RegexAssembly { get; } = @"\[assembly:\s*(?<Property>.*)\s*\((?<Parameters>.*)\)\]";

		public Property[] ReadProperties(string text)
		{
			var matches = Regex.Matches(RemoveComments(text), RegexAssembly);			
			return matches
				.Cast<Match>()
				.Select(x => new { Name = x.Groups["Property"].Value, Parameters = x.Groups["Parameters"].Value })
				.Select(x => new Property {
					Name = x.Name,
					Values = ParseValues(x.Parameters)
				})
				.ToArray();
		}
		
		PropertyValue[] ParseValues(string valueText)
		{
			return SplitValues(valueText)
				.Select(token => token.Trim())
				.Select(token => {
					if (token.Length >= 2 && token.First() == '"' && token.Last() == '"') {
						return new PropertyValue {
							Value = token.Substring(1, token.Length-2),
							Type = PropertyValueType.String,
						};
					}
					else {
						return new PropertyValue {
							Value = token,
							Type = PropertyValueType.Unknown,
						};
					}
				})
				.ToArray();			
		}

		string[] SplitValues(string valueText)
		{
			var values = new List<string>();
			var length = valueText.Length;
			var inString = false;
			var startIndex = 0;

			for (int i = 0; i < length - 1; i++) {
				var current = valueText[i];
				var next = valueText[i + 1];

				if (current == '"') {
					inString = !inString;
				}
				else if (current == '\\') {
					i++;
					continue;
				}
				else if (current == ',') {
					var tokenLength = i - startIndex;
					if (tokenLength == 0) {
						values.Add(string.Empty);
					}
					else {
						values.Add(valueText.Substring(startIndex, tokenLength));
					}
					startIndex = i+1;
				}
			}
			
			if (length - 1 - startIndex > 0) {
				values.Add(valueText.Substring(startIndex));
			}

			return values.ToArray();
		}

		string RemoveComments(string text)
		{
			var sb = new StringBuilder();
			var length = text.Length;
			var inString = false;
			var inComment = false;	

			for (int i = 0; i < length - 1; i++) {
				var current = text[i];
				var next = text[i + 1];

				if (inComment) {
					if (current == '\n' || current == '\r') {
						inComment = false;
					}
					else {
						continue;
					}
				}

				if (current == '"') {
					inString = !inString;
					sb.Append(current);
				}
				else if (current == '\\') {
					i++;
					sb.Append(current);
					sb.Append(next);
					continue;
				}
				else if (!inString && current == '/' && next == '/') {
					inComment = true;
				}
				else {
					sb.Append(current);
				}
			}
			
			if (!inComment && text.Length > 0) {
				sb.Append(text.Last());
			}

			return sb.ToString();
		}
	}

	public class Property
	{
		public string Name { get; set; }
		public PropertyValue[] Values { get; set; }
	}

	public class PropertyValue {
		public string Value { get; set; }
		public PropertyValueType Type { get; set; }
	}

	public enum PropertyValueType
	{
		Unknown,
		String,
	}
}
