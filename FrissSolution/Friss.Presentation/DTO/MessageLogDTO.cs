using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Friss.Presentation.DTO
{
	public class MessageLogDTO
	{
		public string RequestContentType { get; set; }
		public string RequestUri { get; set; }
		public string RequestMethod { get; set; }
		public DateTime RequestDateTime { get; set; }
		public string ResponseContentType { get; set; }
		public HttpStatusCode ResponseStatusCode { get; set; }
		public DateTime ResponseDateTime { get; set; }
	}
}