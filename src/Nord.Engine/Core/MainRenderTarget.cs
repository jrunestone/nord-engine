using SFML.Graphics;

namespace Nord.Engine.Core;

public class MainRenderTarget
{
    public RenderTexture? RenderTexture { get; private set; }
    internal Sprite? Sprite { get; private set; }

    internal void Create(uint width, uint height)
    {
        RenderTexture = new RenderTexture(width, height);
        Sprite = new Sprite(RenderTexture.Texture);
    }
}