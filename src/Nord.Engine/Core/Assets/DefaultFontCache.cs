using Nord.Engine.Core.Configuration;
using SFML.Graphics;

namespace Nord.Engine.Core.Assets;

public class DefaultFontCache : DefaultAssetCache<Font>, IFontCache
{
    public Font DefaultFont => GetFont(_options.DefaultFontName);
    private readonly EngineOptions _options;

    public DefaultFontCache(EngineOptions options) : base(options)
    {
        _options = options;
    }
    
    public Font GetFont(string filename)
    {
        return GetAsset(filename, x => new Font(x));
    }
}