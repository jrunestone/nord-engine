using Nord.Engine.Core.Configuration;
using Nord.Engine.Core.Extensions;
using Nord.Engine.Core.Rendering;
using Nord.Engine.Scenes;
using SFML.Graphics;
using SFML.System;

namespace Nord.Engine.Core;

public class DefaultApplication : IApplication
{
    private readonly EngineOptions _options;
    private readonly Time _time;
    private readonly RenderWindow _window;
    private readonly ISceneService _sceneService;
    private readonly IMainRenderTarget _mainRenderTarget;
    private readonly IEnumerable<IGlobalProcess> _processes;

    public DefaultApplication(
        EngineOptions options,
        Time time,
        RenderWindow window,
        ISceneService sceneService,
        IMainRenderTarget mainRenderTarget,
        IEnumerable<IGlobalProcess> processes)
    {
        _options = options;
        _time = time;
        _window = window;
        _sceneService = sceneService;
        _mainRenderTarget = mainRenderTarget;
        _processes = processes;
    }
    
    public virtual void Run()
    {
        _window.Size = new Vector2u(_options.VideoMode.Width, _options.VideoMode.Height);
        _window.SetTitle(_options.Name);
        _window.SetVisible(true);
        _window.Closed += (_, _) => _window.Close();
        
        _mainRenderTarget.Create(_window.Size.X, _window.Size.Y);

        while (_window.IsOpen)
        {
            _time.Update();
            _window.DispatchEvents();
            // window.Clear(new Color(46, 52, 64));
            
            _mainRenderTarget.RenderTexture!.Clear(Colors.Clear);
            
            _processes.ForEach(x => x.Update(_time));
            _sceneService.CurrentScene?.Update(_time);
            
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