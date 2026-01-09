using System;
using System.Text.Json;
using ManagerConsole.Models;

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

    private static ICollection<UserProfile> GetProfiles()
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
