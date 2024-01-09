using SFML.Graphics;
using SFML.System;

namespace Nord.Engine.Ecs.Components;

public class TextComponent : IDrawableComponent
{
    public Drawable Drawable => Text;
    public uint? TextureId => null;
    
    public Text Text { get; }
    public string OriginalString { get; }

    public TextComponent(string text, Font font, Vector2f position, uint? size = null, Color? color = null)
    {
        OriginalString = text;
        
        Text = new(text, font, size ?? 20)
        {
            Position = position,
            FillColor = color ?? new Color(216, 222, 233)
        };
    }
}