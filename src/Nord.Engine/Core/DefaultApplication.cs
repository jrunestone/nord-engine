using Nord.Engine.Core.Configuration;
using Nord.Engine.Scenes;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Nord.Engine.Core;

public class DefaultApplication(
    EngineOptions options,
    Clock clock,
    MainRenderTarget mainRenderTarget,
    ISceneService sceneService) : IApplication
{
    private readonly EngineOptions _options = options;
    private readonly Clock _clock = clock;
    private readonly MainRenderTarget _mainRenderTarget = mainRenderTarget;
    private readonly ISceneService _sceneService = sceneService;

    public virtual void Run()
    {
        var window = new RenderWindow(_options.GetVideoMode(), _options.Name, Styles.Default);
        window.Closed += (_, _) => window.Close();
        
        _mainRenderTarget.Create(window.Size.X, window.Size.Y);
        
        while (window.IsOpen)
        {
            var time = _clock.Restart();
            
            window.DispatchEvents();
            // window.Clear(new Color(46, 52, 64));

            
            _mainRenderTarget.RenderTarget!.Clear(new Color(46, 52, 64));
            _sceneService.CurrentScene?.Update(time.AsSeconds());
            _mainRenderTarget.RenderTarget!.Draw(new CircleShape(100f) { FillColor = new Color(67, 76, 94) });
            _mainRenderTarget.RenderTarget!.Display();
            
            window.Draw(_mainRenderTarget.Sprite);
            window.Display();
        }
    }
}