using Nord.Engine.Core.Configuration;
using SFML.Graphics;

namespace Nord.Engine.Core.Assets;

public class DefaultTextureCache(EngineOptions options) : DefaultAssetCache<Texture>(options), ITextureCache
{
    public Texture GetTexture(string filename)
    {
        return GetAsset(filename, x => new Texture(x));
    }
}