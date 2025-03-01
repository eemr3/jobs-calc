using System.Net;

namespace JobsCalc.Application.Exceptions;

public class FileFormatNotAllowedException : JobsCalcException
{
  public FileFormatNotAllowedException(string message) : base(message){}

  public override List<string> GetErrorMessages() => [Message];

  public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}