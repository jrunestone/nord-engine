using Nord.Engine.Core.Configuration;
using SFML.Graphics;

namespace Nord.Engine.Core;

public class DefaultTextureCache(EngineOptions options) : ITextureCache
{
    private readonly IDictionary<string, Texture> _cache = new Dictionary<string, Texture>();
    private readonly EngineOptions _options = options;
    
    public Texture GetTexture(string filename)
    {
        var key = filename.ToLowerInvariant();

        if (_cache.TryGetValue(key, out var texture))
        {
            return texture;
        }
        
        texture = new Texture(Path.Combine(_options.AssetRootPath!, filename));
        _cache.Add(key, texture);

        return texture;
    }
}