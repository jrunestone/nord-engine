using SFML.Graphics;

namespace Nord.Engine.Core.Assets;

public interface IFontCache
{
    Font DefaultFont { get; }
    Font GetFont(string filename);
}