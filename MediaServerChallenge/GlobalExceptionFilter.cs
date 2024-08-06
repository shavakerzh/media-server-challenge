using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MediaServerChallenge;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "An unhandled exception has occurred.");

        var response = new
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Message = "Internal Server Error. Please try again later.",
        };

        context.Result = new JsonResult(response)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };

        context.ExceptionHandled = true;
    }
}