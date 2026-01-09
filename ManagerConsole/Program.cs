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
        string userName = AnsiConsole.Ask<string>("Enter your user name: ");
        string userEmail = AnsiConsole.Ask<string>("Enter your email: ");

        AddUser(userName, userEmail);
    }
    else if (choice == "Get profles")
    {
        
    }
    else if (choice == "Exit")
    {
        break;
    }
    AnsiConsole.Clear();
}

