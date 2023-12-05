using SFML.Graphics;
using SFML.System;

namespace Nord.Engine.Ecs.Components;

public class TextComponent
{
    public Text Text { get; init; }

    public TextComponent(string text, Font font, Vector2f position, uint? size = null, Color? color = null)
    {
        Text = new(text, font, size ?? 20)
        {
            Position = position,
            FillColor = color ?? new Color(216, 222, 233)
        };
    }
}