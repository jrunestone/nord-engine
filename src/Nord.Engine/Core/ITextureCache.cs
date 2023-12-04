using SFML.Graphics;

namespace Nord.Engine.Core;

public interface ITextureCache
{
    Texture GetTexture(string filename);
}