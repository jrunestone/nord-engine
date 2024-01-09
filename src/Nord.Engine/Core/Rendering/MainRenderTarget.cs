using SFML.Graphics;

namespace Nord.Engine.Core.Rendering;

public class MainRenderTarget : IMainRenderTarget
{
    public RenderTexture? RenderTexture { get; private set; }
    public Sprite? Sprite { get; private set; }

    public void Create(uint width, uint height)
    {
        RenderTexture = new RenderTexture(width, height);
        Sprite = new Sprite(RenderTexture.Texture);
    }
}