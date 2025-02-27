using System.Net;

namespace JobsCalc.Application.Exceptions;

public class ConflictException : JobsCalcException
{
    public ConflictException(string message) : base(message){}

    public override List<string> GetErrorMessages()
    {
        return [Message];
    }

    public override HttpStatusCode GetStatusCode()
    {
        return HttpStatusCode.Conflict;
    }
}