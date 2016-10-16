using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studiotaiha.Toolkit.Rest.Tests
{
	public class StubHeaderBag : Dictionary<string, string>, IHeaderBag
	{
	}
}
