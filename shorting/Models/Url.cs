using System.ComponentModel.DataAnnotations;

namespace shorting.Models;

// UrlDto to url creation
public class Url
{
    [Required]
    public string UrlToShorten { get; set; }
    
    [Required]
    public string CurrentUrl { get; set; }
    
    [Required]
    public string ExtraOptions { get; set; }
}