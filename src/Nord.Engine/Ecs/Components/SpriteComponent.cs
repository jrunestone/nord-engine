using SFML.Graphics;

namespace Nord.Engine.Ecs.Components;

public class SpriteComponent(Texture texture)
{
    public Sprite Sprite { get; } = new(texture);
}