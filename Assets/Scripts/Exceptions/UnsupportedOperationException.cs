using System;
using System.Runtime.Serialization;

public class UnsupportedOperationException : Exception
{

    public UnsupportedOperationException() : base()
    {
    }

    public UnsupportedOperationException(string message) : base(message)
    {
    }

    public UnsupportedOperationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected UnsupportedOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

}
