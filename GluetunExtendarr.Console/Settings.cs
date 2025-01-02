#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
using Microsoft.Extensions.Configuration;

namespace GluetunExtendarr.Console;

public record Settings
{
    [ConfigurationKeyName("CONFIG_NAME")]
    public string ConfigName { get; set; }
    
    [ConfigurationKeyName("INPUT_DIR")]
    public string InputDir { get; set; }

    [ConfigurationKeyName("OUTPUT_DIR")]
    public string OutputDir { get; set; }
}