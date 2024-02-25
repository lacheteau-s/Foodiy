using System.ComponentModel.DataAnnotations;

namespace Foodiy.Api.Configuration.Options;

public record AzureStorageOptions
{
    public const string SectionName = "AzureStorage";

    [Required]
    public string ConnectionString { get; set; } = null!;

    [Required]
    public string ShareName { get; set; } = null!;

    [Required]
    public string FileName { get; set; } = null!;
}
