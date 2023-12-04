using SFML.Graphics;

namespace Nord.Engine.Core;

public class DefaultTextureCache : ITextureCache
{
    private readonly IDictionary<string, Texture> _cache = new Dictionary<string, Texture>();
    
    public Texture GetTexture(string filename)
    {
        var key = filename.ToLowerInvariant();

        if (_cache.TryGetValue(key, out var texture))
        {
            return texture;
        }
        
        texture = new Texture(filename);
        _cache.Add(key, texture);

        return texture;
    }
}