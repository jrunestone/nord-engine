using SFML.Graphics;

namespace Nord.Engine.Core.Rendering;

public interface IRenderTarget
{
    RenderTexture? RenderTexture { get; }
    Sprite? Sprite { get; }
    void Create(uint width, uint height);
}