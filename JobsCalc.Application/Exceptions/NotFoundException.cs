using System.Net;

namespace JobsCalc.Application.Exceptions;

public class NotFoundException : JobsCalcException
{
    public NotFoundException(string message) : base(message){}

    public override List<string> GetErrorMessages()
    {
        return [Message];
    }

    public override HttpStatusCode GetStatusCode()
    {
        return HttpStatusCode.NotFound;
    }
}