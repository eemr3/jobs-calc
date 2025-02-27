using System.Net;

namespace JobsCalc.Application.Exceptions;

public class ErrorOnValidationException : JobsCalcException
{
    private readonly List<string> _errors;
    public ErrorOnValidationException(List<string> errorMessages) : base(string.Empty)
    {
        _errors = errorMessages;
    }

    public override List<string> GetErrorMessages()
    {
        return _errors;
    }

    public override HttpStatusCode GetStatusCode()
    {
        return HttpStatusCode.BadRequest;
    }
}