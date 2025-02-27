using System.Net;

namespace JobsCalc.Application.Exceptions;

public abstract class JobsCalcException : SystemException
{
    public JobsCalcException(string message): base(message){}
    
    public abstract List<string> GetErrorMessages();
    public abstract HttpStatusCode GetStatusCode();
}