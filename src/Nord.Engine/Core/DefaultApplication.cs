using Nord.Engine.Core.Configuration;
using Nord.Engine.Core.Extensions;
using Nord.Engine.Scenes;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Nord.Engine.Core;

public class DefaultApplication : IApplication
{
    private readonly EngineOptions _options;
    private readonly Clock _clock;
    private readonly MainRenderTarget _mainRenderTarget;
    private readonly ISceneService _sceneService;
    private readonly IEnumerable<IGlobalProcess> _processes;

    public DefaultApplication(
        EngineOptions options,
        Clock clock,
        MainRenderTarget mainRenderTarget,
        ISceneService sceneService,
        IEnumerable<IGlobalProcess> processes)
    {
        _options = options;
        _clock = clock;
        _mainRenderTarget = mainRenderTarget;
        _sceneService = sceneService;
        _processes = processes;
    }
    
    public virtual void Run()
    {
        var window = new RenderWindow(_options.GetVideoMode(), _options.Name, Styles.Default);
        window.Closed += (_, _) => window.Close();
        
        _mainRenderTarget.Create(window.Size.X, window.Size.Y);
        
        while (window.IsOpen)
        {
            var time = _clock.Restart();
            var dt = time.AsSeconds();
            
            window.DispatchEvents();
            // window.Clear(new Color(46, 52, 64));
            
            _mainRenderTarget.RenderTexture!.Clear(new Color(46, 52, 64));
            _processes.ForEach(x => x.Update(dt));
            _sceneService.CurrentScene?.Update(dt);
            _mainRenderTarget.RenderTexture!.Display();
            
            window.Draw(_mainRenderTarget.Sprite);
            window.Display();
        }
    }
}