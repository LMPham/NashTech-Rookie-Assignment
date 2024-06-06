namespace Application.Common.Interfaces
{
    /// <summary>
    /// Base interface for users.
    /// </summary>
    public interface IUser
    {
        string? Id { get; }
        string? UserName { get; }
    }
}
