namespace Notissimus.Domain.Exceptions;

public class DataProviderException : Exception
{
    public DataProviderException(string? message) : base(message) { }
}