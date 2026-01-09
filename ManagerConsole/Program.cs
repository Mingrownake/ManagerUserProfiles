using ManagerConsole.Enum;
using Spectre.Console;

while(true)
{
    AnsiConsole.Clear();
    AnsiConsole.MarkupLine("[green]Welcome to the HR Dashboard![/]");
    AnsiConsole.WriteLine();
    var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
        .Title("Select action!")
        .AddChoices("Add User","Edit profles", "Exit"));
    if (choice == "Add User")
    {
        string userName = AnsiConsole.Ask<string>("Enter your name: ");
        string userLastName = AnsiConsole.Ask<string>("Enter your last name: ");

        AddUser(userName, userLastName);
    }
    else if (choice == "Edit profles")
    {
        var profiles = GetProfiles();  
        if (profiles.Count > 0)
        {
            DrawUserProfileTable(profiles);
            var selectedUser = SelectUserProfile(profiles);
            if (selectedUser is null)
            {
                continue;
            }
            AnsiConsole.Clear();
            var choiceForEdit = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Select the field you want to edit")
                .AddChoices("User name","Last user name", "User email", "Exit"));
            if (choiceForEdit == "User name")
            {
                EditUserProfile(selectedUser, EditingProperty.UserName);
            }
            else if (choiceForEdit == "Last user name")
            {
                EditUserProfile(selectedUser, EditingProperty.UserLastName);
            }
            else if(choiceForEdit == "User email")
            {
                EditUserProfile(selectedUser, EditingProperty.UserEmail);
            }
            else if (choiceForEdit == "Exit")
            {
                continue;
            }
            AnsiConsole.MarkupLine("[green]User profile successfully updated![/]");
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
}

