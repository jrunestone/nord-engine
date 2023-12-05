using Nord.Engine.Core.Configuration;

namespace Nord.Engine.Core.Assets;

public class DefaultAssetCache<T>(EngineOptions options)
    where T : class
{
    private readonly IDictionary<string, T> _cache = new Dictionary<string, T>();
    private readonly EngineOptions _options = options;
    
    protected T GetAsset(string filename, Func<string, T> activateAsset)
    {
        var key = filename.ToLowerInvariant();

        if (_cache.TryGetValue(key, out var asset))
        {
            return asset;
        }
        
        asset = activateAsset(Path.Combine(_options.AssetRootPath, filename));
        _cache.Add(key, asset);

        return asset;
    }
}