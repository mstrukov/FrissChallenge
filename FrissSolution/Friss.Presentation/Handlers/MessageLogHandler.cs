using Friss.Infrastructure.Logging;
using Friss.Presentation.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Unity;

namespace Friss.Presentation.Handlers
{
	public class MessageLogHandler : DelegatingHandler
	{
		private IFrissLogger _logger;

		public MessageLogHandler()
		{
			_logger = UnityConfig.Container.Resolve<IFrissLogger>();
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var messageLog = GetRequestMetadata(request);
			var response = await base.SendAsync(request, cancellationToken);
			messageLog = GetResponseMetadata(messageLog, response);
			// Don't do await for the method, 
			// it should be done in a fire and forget way not to affect performance
			// and should be configured/done by a log provider
			// I use NLog and it can be easily configured to do async logging by changing the config
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
		private void SendToLog(MessageLogDTO messageLog)
		{
			_logger.LogInfo(messageLog);
		}
	}
}