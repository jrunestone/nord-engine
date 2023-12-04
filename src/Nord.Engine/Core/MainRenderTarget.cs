using SFML.Graphics;

namespace Nord.Engine.Core;

public class MainRenderTarget
{
    public RenderTexture? RenderTarget { get; private set; }
    internal Sprite? Sprite { get; private set; }

    internal void Create(uint width, uint height)
    {
        RenderTarget = new RenderTexture(width, height);
        Sprite = new Sprite(RenderTarget.Texture);
    }
}