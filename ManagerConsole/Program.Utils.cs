using System;
using System.Text.Json;

partial class Program
{
    private static JsonSerializerOptions GetSerializerOptions()
    {
        JsonSerializerOptions option = new JsonSerializerOptions()
        {
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
          WriteIndented = true  
        };
        return option;
    }
}
