using Nord.Engine.Core.Configuration;
using SFML.Graphics;
using SFML.Window;

namespace Nord.Engine.Core;

public class DefaultApplication(
    EngineOptions _options) : IApplication
{
    public virtual void Run()
    {
        var window = new RenderWindow(_options.GetVideoMode(), _options.Name, Styles.Default);
        
        window.Closed += (_, _) => window.Close();
        
        while (window.IsOpen)
        {
            window.DispatchEvents();
            window.Clear(new Color(46, 52, 64));
            
            // TODO: CurrentScene.
            window.Draw(new CircleShape(100f) { FillColor = new Color(67, 76, 94) });
            
            window.Display();
        }
    }
}