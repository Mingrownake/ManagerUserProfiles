namespace ManagerConsole.Models;

public class UserProfile
{
    public string Uuid {get; init;} = Guid.NewGuid().ToString();
    public required string UserName { get; init; }
    public required string UserLastName {get; init;}
    public string Email { get; set; } = string.Empty;
    public DateOnly RegisterDate {get; init;} = DateOnly.FromDateTime(DateTime.Now);
}
