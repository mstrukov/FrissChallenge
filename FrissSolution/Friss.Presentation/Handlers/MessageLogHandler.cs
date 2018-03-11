using Friss.Presentation.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Friss.Presentation.Handlers
{
	public class MessageLogHandler : DelegatingHandler
	{
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var messageLog = GetRequestMetadata(request);
			var response = await base.SendAsync(request, cancellationToken);
			messageLog = GetResponseMetadata(messageLog, response);
			// Explicitly don't do await for log write operation, 
			// usually it should be done in a fire and forget way not to affect performance
			SendToLog(messageLog);
			return response;
		}
		private MessageLogDTO GetRequestMetadata(HttpRequestMessage request)
		{
			MessageLogDTO log = new MessageLogDTO
			{
				RequestMethod = request.Method.Method,
				RequestDateTime = DateTime.UtcNow,
				RequestUri = request.RequestUri.ToString()
			};
			return log;
		}
		private MessageLogDTO GetResponseMetadata(MessageLogDTO messageLog, HttpResponseMessage response)
		{
			messageLog.ResponseStatusCode = response.StatusCode;
			messageLog.ResponseDateTime = DateTime.UtcNow;
			messageLog.ResponseContentType = response.Content.Headers.ContentType.MediaType;
			return messageLog;
		}
		private async Task SendToLog(MessageLogDTO messageLog)
		{
			// TODO: Write code here to store the logMetadata instance to a pre-configured log store...
		}
	}
}