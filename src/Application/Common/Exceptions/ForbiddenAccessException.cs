namespace Application.Common.Exceptions;

/// <summary>
/// Represents an error that occurs when the user is
/// unauthorized to access a resource.
/// </summary>
public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException() : base() { }
}
