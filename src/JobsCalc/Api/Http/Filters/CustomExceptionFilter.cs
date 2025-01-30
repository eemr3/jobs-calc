using JobsCalc.Api.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SystemKeyNotFoundException = System.Collections.Generic.KeyNotFoundException;

namespace JobsCalc.Api.Http.Filters;

public class CustomExceptionFilter : ExceptionFilterAttribute
{
  private readonly ILogger<CustomExceptionFilter> _logger;
  public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
  {
    _logger = logger;
  }
  public override void OnException(ExceptionContext context)
  {
    if (context.Exception is SystemKeyNotFoundException keyNotFoundException)
    {
      var response = new
      {
        StatusCode = 404,
        Error = "NotFound",
        Message = keyNotFoundException.Message,
        Timestamp = DateTime.UtcNow,
        Details = "The requested resource was not found."
      };

      context.Result = new NotFoundObjectResult(response)
      {
        StatusCode = 404
      };
      context.ExceptionHandled = true;
    }
    else if (context.Exception is ConflictException conflictException)
    {
      var response = new
      {
        StatusCode = 409,
        Error = "Conflict",
        Message = conflictException.Message,
        Timestamp = DateTime.UtcNow,
        Details = "The requested resource is in conflict."
      };

      context.Result = new ConflictObjectResult(response)
      {
        StatusCode = 409
      };
      context.ExceptionHandled = true;
    }
    else if (context.Exception is UnauthorizedAccessException unauthorized)
    {
      var response = new
      {
        StatusCode = 401,
        Error = "Unauthorized",
        Message = unauthorized.Message,
        Timestamp = DateTime.UtcNow,
        Details = "You are not authorized to access this resource."
      };

      context.Result = new UnauthorizedObjectResult(response)
      {
        StatusCode = 401
      };
      context.ExceptionHandled = true;
    }
    else if (context.Exception is ForbiddenException forbiddenException)
    {
      var response = new
      {
        StatusCode = 403,
        Error = "Forbidden",
        Message = forbiddenException.Message,
        Timestamp = DateTime.UtcNow,
        Details = "Access prohibited for this resource."
      };

      context.Result = new ObjectResult(response)
      {
        StatusCode = 403
      };
      context.ExceptionHandled = true;
    }
    else
    {
      // Dados principais
      var exception = context.Exception;
      var request = context.HttpContext.Request;
      var user = context.HttpContext.User?.Identity?.Name ?? "Anonymous";

      _logger.LogError(exception,
          "Unhandled Exception occurred.\n" +
          "Message: {Message}\n" +
          "Type: {Type}\n" +
          "Stack Trace: {StackTrace}\n" +
          "Timestamp: {Timestamp}\n" +
          "Request Path: {RequestPath}\n" +
          "HTTP Method: {HttpMethod}\n" +
          "Query String: {QueryString}\n" +
          "User: {User}",
          exception.Message,
          exception.GetType().FullName,
          exception.StackTrace,
          DateTime.UtcNow,
          request.Path,
          request.Method,
          request.QueryString,
          user
      );

      context.Result = new ObjectResult(context.Exception.Message)
      {
        StatusCode = 500
      };
      context.ExceptionHandled = true;
    }
  }
}