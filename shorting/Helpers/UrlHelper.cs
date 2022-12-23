namespace shorting.Helpers;

public static class UrlHelper
{
    public static string GetUrl()
    {
        var shortenedSegment = Guid.NewGuid().GetHashCode().ToString("X");
        return shortenedSegment;
    }
}