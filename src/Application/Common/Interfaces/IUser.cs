namespace Application.Common.Interfaces;

/// <summary>
/// Base interface for users.
/// </summary>
public interface IUser
{
    public string? Id { get; }
    public string? UserName { get; }
    public static string? Mode { get; set; }
    public bool IsInRole(string roleName);
}
