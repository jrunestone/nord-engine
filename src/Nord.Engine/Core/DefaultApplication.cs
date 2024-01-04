using Nord.Engine.Core.Bus;
using Nord.Engine.Core.Configuration;
using Nord.Engine.Core.Extensions;
using Nord.Engine.Scenes;
using SFML.Graphics;
using SFML.System;

namespace Nord.Engine.Core;

public class DefaultApplication : IApplication
{
    private readonly EngineOptions _options;
    private readonly Clock _clock;
    private readonly RenderWindow _window;
    private readonly MainRenderTarget _mainRenderTarget;
    private readonly IGlobalBus _bus;
    private readonly ISceneService _sceneService;
    private readonly IEnumerable<IGlobalProcess> _processes;

    public DefaultApplication(
        EngineOptions options,
        RenderWindow window,
        MainRenderTarget mainRenderTarget,
        Clock clock,
        IGlobalBus bus,
        ISceneService sceneService,
        IEnumerable<IGlobalProcess> processes)
    {
        _options = options;
        _window = window;
        _mainRenderTarget = mainRenderTarget;
        _clock = clock;
        _bus = bus;
        _sceneService = sceneService;
        _processes = processes;
    }
    
    public virtual void Run()
    {
        _window.Size = new Vector2u(_options.VideoMode.Width, _options.VideoMode.Height);
        _window.SetView(new View(new FloatRect(0, 0, _options.VideoMode.Width, _options.VideoMode.Height)));
        _window.SetTitle(_options.Name);
        _window.SetVisible(true);
        _window.Closed += (_, _) => _window.Close();
        
        _mainRenderTarget.Create(_window.Size.X, _window.Size.Y);

        while (_window.IsOpen)
        {
            var time = _clock.Restart();
            var dt = time.AsSeconds();
            
            _window.DispatchEvents();
            // window.Clear(new Color(46, 52, 64));
            
            _mainRenderTarget.RenderTexture!.Clear(new Color(46, 52, 64));
            _processes.ForEach(x => x.Update(dt));
            _sceneService.CurrentScene?.Update(dt);
            _mainRenderTarget.RenderTexture!.Display();
            
            _window.Draw(_mainRenderTarget.Sprite);
            _window.Display();
        }
    }

    public virtual void Exit()
    {
        _window.Close();
    }
}