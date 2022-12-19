namespace shorting.Helpers;

public static class SettingsHelper
{
    
    public static void GetExpirationOption(string key)
    {
        //var hey = GetExpirationOptions.Values.FirstOrDefault(x => x == key);
     
    }

    public static Dictionary<string, int> GetExpirationOptions()
    {
        // key text for selection - minutes
        var options = new Dictionary<string, int>()
        {
            { "5 minutes" , 5},
            { "10 minutes", 10 },
            { "30 minutes", 30 },
            { "1 hour", 60 },
            { "1 day", 1440 },
            { "1 week", 10080 }
        };
        return options;
    }

}