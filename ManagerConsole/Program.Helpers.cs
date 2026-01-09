using System;
using System.Text.Json;
using ManagerConsole.Enum;
using ManagerConsole.Models;
using Spectre.Console;

partial class Program
{
    private static bool AddUser(string name, string lastName)
    {
        UserProfile userProfile = new()
        {
            UserName = name,
            UserLastName = lastName
        };
        CreateProfileFile(userProfile);
        return true;
    }

    private static IList<UserProfile> GetProfiles()
    {
        var directoryPath = GetDirectiryPath();
        var directoryInfo = new DirectoryInfo(directoryPath);

        JsonSerializerOptions option = GetSerializerOptions();
        var usersProfiles = new List<UserProfile>();

        var filesInfo = directoryInfo.GetFiles();
        foreach (var info in filesInfo)
        {
            using FileStream fileStream = info.Open(FileMode.Open);
            var userProfile = JsonSerializer.Deserialize<UserProfile>(utf8Json: fileStream, option);
            if (userProfile is not null)
            {
                usersProfiles.Add(userProfile);
            }
        }
        return usersProfiles;
    }

    private static void CreateProfileFile(UserProfile userProfile)
    {
        var directoryPath = GetDirectiryPath();
        var filePath = Path.Combine(directoryPath, $"{userProfile.Uuid}.json");
        JsonSerializerOptions option = GetSerializerOptions();
        using FileStream fileStream = File.Create(filePath);
        JsonSerializer.Serialize(utf8Json: fileStream, value: userProfile, option);
    }

    private static void DrawUserProfileTable(IList<UserProfile> profiles)
    {
        var table = new Table();
            var typeUserProfile = typeof(UserProfile);
            var userProfileFields = typeUserProfile.GetProperties();

            foreach (var propetry in userProfileFields)
            {
                if (propetry.Name == "Uuid")
                {
                    table.AddColumn("Id");
                } else
                {
                    table.AddColumn(propetry.Name); 
                }
                
            }

            for (int i = 0; i < profiles.Count; i++)
            {
                var profile = profiles[i];
                table.AddRow(
                    $"{i + 1}",
                    profile.UserName,
                    profile.UserLastName,
                    profile.Email,
                    profile.RegisterDate.ToString()
                );
            }
            AnsiConsole.Write(table);
    }
    
    private static UserProfile? SelectUserProfile(IList<UserProfile> profiles)
    {
        int selectProfileId = AnsiConsole.Ask<int>("Enter user id / or 0 for exit: ");
        if (selectProfileId == 0)
        {
            return null;
        }

        if (selectProfileId <= -1 || selectProfileId > profiles.Count)
        {
            throw new ArgumentException($"Selected id not fount: {selectProfileId}");
        }
        return profiles[selectProfileId - 1];
    }

    private static void EditUserProfile(UserProfile userProfile, EditingProperty property)
    {
        string answer = string.Empty;
        switch (property)
        {
            case EditingProperty.UserName:
                answer = AnsiConsole.Ask<string>("Enter new name: ");
                userProfile.UserName = answer;
                break;
            case EditingProperty.UserLastName:
                answer = AnsiConsole.Ask<string>("Enter new last name: ");
                userProfile.UserLastName = answer;
                break;
            case EditingProperty.UserEmail:
                answer = AnsiConsole.Ask<string>("Enter new email: ");
                userProfile.Email = answer;
                break;
        }
        UpdateUserProfileFile(userProfile);
    }

    private static void UpdateUserProfileFile(UserProfile userProfile)
    {
        CreateProfileFile(userProfile);
    }

    private static string GetDirectiryPath()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Profiles");
        if (!Path.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        return path;
    }
}
