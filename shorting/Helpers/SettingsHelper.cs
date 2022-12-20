namespace shorting.Helpers;

public static class SettingsHelper
{
    private static Dictionary<string, int> options = new()
    {
        { "None" , 0},
        { "5 minutes" , 5},
        { "10 minutes", 10 },
        { "30 minutes", 30 },
        { "1 hour", 60 },
        { "1 day", 1440 },
        { "1 week", 10080 }
    };

    public static int GetExpirationOption(this string key) => options[key];

    public static Dictionary<string, int> GetExpirationOptions() => options;
    
}