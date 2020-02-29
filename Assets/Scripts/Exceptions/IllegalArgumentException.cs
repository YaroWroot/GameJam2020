using System;
using System.Runtime.Serialization;

public class IllegalArgumentException : Exception
{

    public IllegalArgumentException() : base()
    {
    }

    public IllegalArgumentException(string message) : base(message)
    {
    }

    public IllegalArgumentException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected IllegalArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

}
