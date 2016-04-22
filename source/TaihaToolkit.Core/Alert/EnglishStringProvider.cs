using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Toolkit.Alert
{
	internal class EnglishStringProvider : IAlertStringProvider
	{
		public string Error
		{
			get
			{
				return "Error";
			}
		}

		public string Warning
		{
			get
			{
				return "Warning";
			}
		}

		public string Information
		{
			get
			{
				return "Information";
			}
		}

		public string Exclamation
		{
			get
			{
				return "注意";
			}
		}

		public string Debug
		{
			get
			{
				return "Debug";
			}
		}

		public string Question
		{
			get
			{
				return "Question";
			}
		}

		public string Yes
		{
			get
			{
				return "Yes";
			}
		}

		public string No
		{
			get
			{
				return "No";
			}
		}

		public string Ok
		{
			get
			{
				return "OK";
			}
		}

		public string Cancel
		{
			get
			{
				return "Cancel";
			}
		}

		public string Select
		{
			get
			{
				return "Select";
			}
		}


		public string Confirmation
		{
			get
			{
				return "Confirmation";
			}
		}
	}
}
