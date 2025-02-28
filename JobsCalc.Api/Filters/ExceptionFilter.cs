using JobsCalc.Application.Exceptions;
using JobsCalc.Communication.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JobsCalc.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
  public void OnException(ExceptionContext context)
  {
    if (context.Exception is JobsCalcException jobsCalcException)
    {
      context.HttpContext.Response.StatusCode = (int)jobsCalcException.GetStatusCode();
      context.Result = new ObjectResult(new ErrorMessagesResponse
      {
        Errors = jobsCalcException.GetErrorMessages()
      });
    }
    else
    {
      context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

      context.Result = new ObjectResult(new ErrorMessagesResponse
      {
        Errors = ["Ocorreu um erro interno!"]
      });
    }
  }
}