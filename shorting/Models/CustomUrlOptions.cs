namespace shorting.Models;

[GenerateSerializer]
public class CustomUrlOptions
{
    [Id(0)]
    public int ExpirationMinutes { get; set; }
    [Id(1)]
    public int AccessAmount { get; set; }
}