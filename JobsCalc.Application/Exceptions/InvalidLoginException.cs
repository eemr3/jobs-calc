using System.Net;

namespace JobsCalc.Application.Exceptions;

public class InvalidLoginException : JobsCalcException
{
    public InvalidLoginException(string message) : base("Email e/ou senha inválidos"){}

    public override List<string> GetErrorMessages()
    {
        return [Message];
    }

    public override HttpStatusCode GetStatusCode()
    {
        return HttpStatusCode.Unauthorized;
    }
}