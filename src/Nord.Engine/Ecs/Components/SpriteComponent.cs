using SFML.Graphics;
using SFML.System;

namespace Nord.Engine.Ecs.Components;

public class SpriteComponent
{
    public Sprite Sprite { get; init; }

    public SpriteComponent(Texture texture)
    {
        Sprite = new(texture);
    }
    
    public SpriteComponent(Texture texture, Vector2f position, IntRect? coords = null)
    {
        Sprite = new(texture, coords ?? new IntRect())
        {
            Position = position
        };
    }
}