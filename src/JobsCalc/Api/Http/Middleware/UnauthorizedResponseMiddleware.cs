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
      await _next(context);

      if (context.Response.StatusCode == 401)
      {
        context.Response.ContentType = "application/json";
        var response = new
        {
          StatusCode = 401,
          Error = "Unauthorized",
          Message = "You need to provide a valid JWT token in the header.",
          Timestamp = DateTime.UtcNow
        };

        var jsonResponse = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(jsonResponse);
      }
    }
  }
}
