using System.Runtime.Serialization;

namespace SharedLibrary.Exceptions;
public class CustomExceptions : Exception
{
    public CustomExceptions()
    {
    }

    public CustomExceptions(string? message) : base(message)
    {
    }

    public CustomExceptions(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected CustomExceptions(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
