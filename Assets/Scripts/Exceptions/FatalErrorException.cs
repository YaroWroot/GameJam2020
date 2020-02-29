using System;
using System.Runtime.Serialization;

public class FatalErrorException : Exception
{

    public FatalErrorException() : base()
    {
    }

    public FatalErrorException(string message) : base(message)
    {
    }

    public FatalErrorException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected FatalErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

}
