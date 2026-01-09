using System.Reflection;
using ManagerConsole.Models;
using Spectre.Console;

while(true)
{
    AnsiConsole.MarkupLine("[green]Welcome to the HR Dashboard![/]");
    AnsiConsole.WriteLine();
    var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
        .Title("Select action!")
        .AddChoices("Add User","Get profles", "Exit"));
    if (choice == "Add User")
    {
        string userName = AnsiConsole.Ask<string>("Enter your name: ");
        string userLastName = AnsiConsole.Ask<string>("Enter your last name: ");

        AddUser(userName, userLastName);
    }
    else if (choice == "Get profles")
    {
        var profiles = GetProfiles();  
        if (profiles.Count > 0)
        {
            var table = new Table();
            var typeUserProfile = typeof(UserProfile);
            var userProfileFields = typeUserProfile.GetProperties();

            foreach (var propetry in userProfileFields)
            {
                table.AddColumn(propetry.Name);
            }

            foreach (var profile in profiles)
            {
                table.AddRow(
                    profile.Uuid.ToString(),
                    profile.UserName,
                    profile.UserLastName,
                    profile.Email,
                    profile.RegisterDate.ToString()
                );
            }
            AnsiConsole.Write(table);
            AnsiConsole.Console.Input.ReadKey(true);
        } 
        else
        {
            AnsiConsole.MarkupLine("[red]User list is empty![/]");
            AnsiConsole.Markup("[white]Press any key...[/]");
            AnsiConsole.Console.Input.ReadKey(true);
        }
    }
    else if (choice == "Exit")
    {
        break;
    }
    AnsiConsole.Clear();
}

