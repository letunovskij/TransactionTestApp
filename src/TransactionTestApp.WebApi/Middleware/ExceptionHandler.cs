using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;
using TransactionTestApp.Common.Exceptions;

namespace TransactionTestApp.WebApi.Middleware;

public sealed class ExceptionHandler(ILogger<ExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken token)
    {      
        if (exception is BusinessErrorException businessError && businessError.ProblemDetails != null)
        {
            businessError.ProblemDetails.Path = context.Request.Path;
            businessError.ProblemDetails.Url = context.Request.GetDisplayUrl();
            businessError.ProblemDetails.ExceptionType = businessError.GetType().ToString();
            logger.LogError(exception, "Exception details: {@ProblemDetails}", businessError.ProblemDetails);

            context.Response.StatusCode = businessError.ProblemDetails?.RequestStatus ?? StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(businessError.ProblemDetails); // перезаписать тело ответа с ошибкой по RFC 9457
        }
        else 
        {
            var message = exception.GetBaseException().Message;
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync(message, token);
            logger.LogError(exception, message);
        }      

        return true;
    }
}
