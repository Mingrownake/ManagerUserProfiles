using System;
using System.Text.Json;
using ManagerConsole.Models;

partial class Program
{
    private static bool AddUser(string name, string email)
    {
        UserProfile userProfile = new()
        {
            UserName = name,
            Email = email
        };
        CreateProfileFile(userProfile);
        return true;
    }

    private static IEnumerable<string> GetProfiles()
    {
        
        return [];
    }

    private static void CreateProfileFile(UserProfile userProfile)
    {
        var directoryPath = GetDirectiryPath();
        var filePath = Path.Combine(directoryPath, $"{userProfile.Uuid}.json");
        JsonSerializerOptions option = new JsonSerializerOptions()
        {
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
          WriteIndented = true  
        };
        string jsonUser = JsonSerializer.Serialize<UserProfile>(userProfile, option);
        File.WriteAllText(filePath, jsonUser);
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
