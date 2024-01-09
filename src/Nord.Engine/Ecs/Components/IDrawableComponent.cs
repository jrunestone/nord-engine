using SFML.Graphics;

namespace Nord.Engine.Ecs.Components;

public interface IDrawableComponent
{
    Drawable Drawable { get; }
    uint? TextureId { get; }
}