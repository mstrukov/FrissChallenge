using Friss.Application.Exceptions;
using Friss.Presentation.DTO;
using System.Net;
using System.Net.Http;

using System.Web.Http.Filters;

namespace Friss.Presentation.Filters
{
	public class AppExceptionFilterAttribute : ExceptionFilterAttribute
	{
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
		}
	}
}