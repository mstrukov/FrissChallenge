using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friss.Application.Exceptions
{
	/// <summary>
	/// Represents exception error codes. This could be extended later to pass more info to the client
	/// in case a business exception occurs 
	/// </summary>
	public enum ErrorCode
	{
		UnknownException = 1
	}
}
