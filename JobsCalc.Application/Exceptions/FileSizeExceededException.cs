using System.Net;

namespace JobsCalc.Application.Exceptions;

public class FileSizeExceededException : JobsCalcException
{
  public FileSizeExceededException(string message) : base(message){}

  public override List<string> GetErrorMessages() => [Message];

  public override HttpStatusCode GetStatusCode() => HttpStatusCode.ExpectationFailed;
}