namespace shorting.Models;

[GenerateSerializer]
public class CustomUrlOptions
{
    [Id(0)]
    public TimeSpan? Expiration { get; set; }
    [Id(1)]
    public int? AccessAmount { get; set; }
}