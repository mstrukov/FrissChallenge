using Friss.Application.Exceptions;
using Friss.Infrastructure.Logging;
using Friss.Presentation.DTO;
using System.Net;
using System.Net.Http;
using Unity;

using System.Web.Http.Filters;

namespace Friss.Presentation.Filters
{
	public class AppExceptionFilterAttribute : ExceptionFilterAttribute
	{
		private IFrissLogger _logger;

		public AppExceptionFilterAttribute()
		{
			_logger = UnityConfig.Container.Resolve<IFrissLogger>();
		}

		public override void OnException(HttpActionExecutedContext context)
		{
			//This handler can be used to map business logic exceptions to WebAPI contract Error DTO to provide client with more info.
			context.Response = context.Request.CreateResponse(
						HttpStatusCode.InternalServerError,
						new ErrorDTO()
						{
							Code = (int)ErrorCode.UnknownException,
							Message = "Something went wrong, please contact your account representative."
						});

			_logger.LogError("Application Unhandled Exception", context.Exception);
		}
	}
}