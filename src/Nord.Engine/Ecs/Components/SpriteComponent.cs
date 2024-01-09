using SFML.Graphics;
using SFML.System;

namespace Nord.Engine.Ecs.Components;

public class SpriteComponent : IDrawableComponent
{
    public Drawable Drawable => Sprite;
    public uint? TextureId => Sprite.Texture.NativeHandle;
    
    public Sprite Sprite { get; }

    public SpriteComponent(Texture texture, Vector2f? position = null, IntRect? coords = null)
    {
        Sprite = new(texture, coords ?? new IntRect())
        {
            Position = position ?? new Vector2f(0, 0)
        };
    }
}