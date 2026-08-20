using ECommerce.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.API.MiddleWare;

public class GlobalExceptionHanlder(IProblemDetailsService problemservice ,
    ILogger<GlobalExceptionHanlder>logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "UnHandled Exception");
        httpContext.Response.StatusCode = exception switch
        {
            ValidationException => StatusCodes.Status400BadRequest,
            ProductNotFoundException => StatusCodes.Status404NotFound,
            BasketNotFoundException => StatusCodes.Status404NotFound,   
            _ => StatusCodes.Status500InternalServerError
        };


        var ProblemDetails = new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Type = exception.GetType().Name,
                Title = "Error has occured",
                Detail = exception.Message

            }


        };
        
        return await problemservice.TryWriteAsync(ProblemDetails);    
    }
}
