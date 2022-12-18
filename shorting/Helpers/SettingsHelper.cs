namespace shorting.Helpers;

public static class SettingsHelper
{
    // int = minutes

    public static void GetExpirationOption(string key)
    {
        //var hey = GetExpirationOptions.Values.FirstOrDefault(x => x == key);
     
    }

    public static Dictionary<int, string> GetExpirationOptions()
    {
        var options = new Dictionary<int, string>()
        {
            { 5, "5 minutes" },
            { 10, "10 minutes" },
            { 30, "30 minutes" },
            { 60, "1 hour" },
            { 1440, "1 day" },
            { 10080, "1 week" }
        };
        return options;
    }

}