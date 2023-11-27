using SFML.Window;

namespace Nord.Engine.Core.Configuration;

public static class EngineOptionsExtensions
{
    public static VideoMode GetVideoMode(this EngineOptions options)
    {
        var resolution = options.Resolution!
            .ToLowerInvariant()
            .Trim()
            .Split("x")
            .Select(uint.Parse)
            .ToArray();

        return new VideoMode(resolution[0], resolution[1]);
    }
}