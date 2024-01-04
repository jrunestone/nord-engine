using SFML.Window;

namespace Nord.Engine.Core.Configuration;

public class EngineOptions
{
    public const string SectionName = "Nord";
    
    public string Name { get; set; } = "nord";
    public string Resolution { get; set; } = "1024x768";
    public bool Windowed { get; set; } = true;
    
    public string DefaultFontName { get; set; } = "fonts/PixelifySans-VariableFont_wght.ttf";
    
    // the absolute path will be automatically prepended to this property
    public string AssetRootPath { get; set; } = "assets";

    private Lazy<VideoMode>? _videoMode;
    public VideoMode VideoMode
    {
        get
        {
            _videoMode ??= new Lazy<VideoMode>(() => ParseVideoMode(Resolution));
            return _videoMode.Value;
        }
    }
    
    private VideoMode ParseVideoMode(string resolution)
    {
        var dimensions = resolution
            .ToLowerInvariant()
            .Trim()
            .Split("x")
            .Select(uint.Parse)
            .ToArray();

        return new VideoMode(dimensions[0], dimensions[1]);
    }
}