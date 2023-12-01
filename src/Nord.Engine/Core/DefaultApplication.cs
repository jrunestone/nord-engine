using Nord.Engine.Core.Configuration;
using Nord.Engine.Scenes;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Nord.Engine.Core;

public class DefaultApplication(
    ISceneService sceneService,
    Clock clock,
    EngineOptions options) : IApplication
{
    private readonly Clock _clock = clock;
    private readonly ISceneService _sceneService = sceneService;
    private readonly EngineOptions _options = options;

    public virtual void Run()
    {
        var window = new RenderWindow(_options.GetVideoMode(), _options.Name, Styles.Default);

        window.Closed += (_, _) => window.Close();
        
        while (window.IsOpen)
        {
            var time = _clock.Restart();
            
            window.DispatchEvents();
            window.Clear(new Color(46, 52, 64));

            _sceneService.CurrentScene?.Update(time.AsSeconds());
            window.Draw(new CircleShape(100f) { FillColor = new Color(67, 76, 94) });

            window.Display();
        }
    }
}