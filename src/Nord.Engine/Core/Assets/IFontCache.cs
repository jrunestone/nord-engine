using SFML.Graphics;

namespace Nord.Engine.Core.Assets;

public interface IFontCache
{
    Font GetFont(string filename);
}