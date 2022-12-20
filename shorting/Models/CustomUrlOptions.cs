namespace shorting.Models;

[GenerateSerializer]
public class CustomUrlOptions
{
    [Id(0)]
    public string? ExpirationKey { get; set; }
    [Id(1)]
    public int? AccessAmount { get; set; }
}