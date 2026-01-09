namespace ManagerConsole.Models;

public class UserProfile
{
    public string Uuid {get; init;} = Guid.NewGuid().ToString();
    public required string UserName { get; init; }
    public required string Email { get; set; }
    public DateOnly RegisterDate {get; init;} = DateOnly.FromDateTime(DateTime.Now);
}
