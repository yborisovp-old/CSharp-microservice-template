using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ServiceTemplate.Middleware;

public class BaseProblemDetailsFactory : ProblemDetailsFactory
{
    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        ProblemDetails? problemDetails = null;
        statusCode ??= 500;
        var context = httpContext.Features.Get<IExceptionHandlerFeature>();

        if (context != null)
        {
            switch (context.Error)
            {
                case KeyNotFoundException notFoundException:
                    statusCode = 404;
                    problemDetails = new ProblemDetails
                    {
                        Status = statusCode,
                        Title = notFoundException.Message,
                        Detail = notFoundException.InnerException?.Message,
                        Instance = instance,
                    };
                    break;
                case InvalidDataException invalidDataException:
                    statusCode = 400;
                    problemDetails = new ProblemDetails
                    {
                        Status = statusCode,
                        Title = invalidDataException.Message,
                        Detail = invalidDataException.InnerException?.Message,
                        Instance = instance,
                    };
                    break;
            }

            httpContext.Response.StatusCode = statusCode.Value;
        }

        if (problemDetails is null)
        {
            problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = context?.Error.Message,
                Type = type,
                Detail = detail,
                Instance = instance,
            };
        }

        return problemDetails;
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        ModelStateDictionary modelStateDictionary,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
    {
        ArgumentNullException.ThrowIfNull(modelStateDictionary);

        statusCode ??= 400;

        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        if (title != null)
        {
            problemDetails.Title = title;
        }

        return problemDetails;
    }
}

