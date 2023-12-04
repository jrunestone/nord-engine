namespace Nord.Engine.Core.Configuration;

public class EngineOptions
{
    public const string SectionName = "Nord";
    
    public string? Name { get; set; } = "nord";
    public string? Resolution { get; set; } = "1024x768";
    public bool Windowed { get; set; } = true;
    public string? AssetRootPath { get; set; } = "assets";
}