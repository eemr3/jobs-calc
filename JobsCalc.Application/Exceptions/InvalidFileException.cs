using System.Net;

namespace JobsCalc.Application.Exceptions;

public class InvalidFileException: JobsCalcException
{
  public InvalidFileException(string message) : base(message){}
  public override List<string> GetErrorMessages() => [Message];

  public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}