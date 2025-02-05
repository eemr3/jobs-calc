using System.Text.Json;


namespace JobsCalc.Api.Http.Middleware
{
  public class UnauthorizedResponseMiddleware
  {
    private readonly RequestDelegate _next;

    public UnauthorizedResponseMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception)
      {
        if (!context.Response.HasStarted)
        {
          context.Response.ContentType = "application/json";
          context.Response.StatusCode = StatusCodes.Status401Unauthorized;
          await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = "Unauthorized" }));
        }
        else
        {
          // Log the error or handle it accordingly
          Console.WriteLine("Response has already started, cannot modify headers.");
        }
      }
    }
  }
}
