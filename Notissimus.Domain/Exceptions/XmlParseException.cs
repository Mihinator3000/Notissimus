namespace Notissimus.Domain.Exceptions;

public class XmlParseException : Exception
{
    public XmlParseException(string? message) : base(message) { }
}