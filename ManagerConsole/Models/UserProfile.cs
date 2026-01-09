namespace ManagerConsole.Models;

public class UserProfile
{
    public string Uuid {get; init;} = Guid.NewGuid().ToString();
    public string UserName { get; set; } = string.Empty;
    public string UserLastName {get; set;} = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly RegisterDate {get; init;} = DateOnly.FromDateTime(DateTime.Now);
}
