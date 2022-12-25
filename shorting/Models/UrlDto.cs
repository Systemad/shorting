namespace shorting.Models;

public class UrlDto
{
    public string ShortenedUrl { get; set; }
    public int ExpirationMinutes { get; set; }
    public int AccessAmount { get; set; }
}