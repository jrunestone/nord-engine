using Nord.Engine.Core.Configuration;
using SFML.Graphics;

namespace Nord.Engine.Core.Assets;

public class DefaultFontCache(EngineOptions options) : DefaultAssetCache<Font>(options), IFontCache
{
    public Font GetFont(string filename)
    {
        return GetAsset(filename, x => new Font(x));
    }
}