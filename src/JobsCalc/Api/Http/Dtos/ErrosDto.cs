namespace JobsCalc.Api.Http.Dtos;
public class NotFoundResponse
{
  public int StatusCode { get; set; }
  public string? Error { get; set; }
  public string? Message { get; set; }
  public DateTime Timestamp { get; set; }
  public string? Details { get; set; }
}

public class UnauthorizedResponse
{
  public int StatusCode { get; set; }
  public string? Error { get; set; }
  public string? Message { get; set; }
  public DateTime Timestamp { get; set; }
  public string? Details { get; set; }
}

public class ConflictResponse
{
  public int StatusCode { get; set; }
  public string? Error { get; set; }
  public string? Message { get; set; }
  public DateTime Timestamp { get; set; }
  public string? Details { get; set; }
}
public class ForbiddenResponse
{
  public int StatusCode { get; set; }
  public string? Error { get; set; }
  public string? Message { get; set; }
  public DateTime Timestamp { get; set; }
  public string? Details { get; set; }
}
