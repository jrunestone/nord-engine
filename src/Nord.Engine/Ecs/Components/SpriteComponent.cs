using SFML.Graphics;
using SFML.System;

namespace Nord.Engine.Ecs.Components;

public class SpriteComponent
{
    public Sprite Sprite { get; init; }
    public uint TextureId => Sprite.Texture.NativeHandle;

    public SpriteComponent(Texture texture, Vector2f? position = null, IntRect? coords = null)
    {
        Sprite = new(texture, coords ?? new IntRect())
        {
            Position = position ?? new Vector2f(0, 0)
        };
    }
}