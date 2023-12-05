using SFML.Graphics;

namespace Nord.Engine.Core.Assets;

public interface ITextureCache
{
    Texture GetTexture(string filename);
}