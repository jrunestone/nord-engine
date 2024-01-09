using System.Diagnostics;
using System.Runtime.InteropServices;
using Arch.Core;
using Nord.Engine.Core;
using Nord.Engine.Core.Bus;
using Nord.Engine.Core.Rendering;
using Nord.Engine.Ecs.Components;
using Nord.Engine.Input;
using SFML.Graphics;
using SFML.Window;
using UltralightNet;
using UltralightNet.AppCore;
using View = UltralightNet.View;

namespace Nord.Engine.Ecs.Systems;

public class DefaultUiRenderSystem : SystemBase
{
    private Renderer? _renderer;
    private View? _view;
    private bool _isLoaded;
    
    private readonly IMainRenderTarget _renderTarget;
    private readonly QueryDescription _query = new QueryDescription()
        .WithAll<UiComponent>();

    public DefaultUiRenderSystem(
        World world,
        IMainRenderTarget renderTarget,
        IBus bus) : base(world)
    {
        _renderTarget = renderTarget;
        bus.Subscribe<KeyDownEvent>(KeyDownHandler);
        bus.Subscribe<TextInputEvent>(TextInputHandler);
    }

    private void KeyDownHandler(KeyDownEvent obj)
    {
        _view!.FireKeyEvent(ULKeyEvent.Create(ULKeyEventType.RawKeyDown, 0, 65, 0, obj.Key.ToString(), obj.Key.ToString(), false, false, false));
    }
    
    private void TextInputHandler(TextInputEvent obj)
    {
        _view!.FireKeyEvent(ULKeyEvent.Create(ULKeyEventType.Char, 0, 65, 0, obj.Character, obj.Character, false, false, false));
    }

    public override void Initialize()
    {
        AppCoreMethods.SetPlatformFontLoader();
        var cfg = new ULConfig();
        _renderer = ULPlatform.CreateRenderer(cfg);
        _view = _renderer.CreateView(1024, 768, new ULViewConfig(){IsTransparent = true, InitialDeviceScale = 1.0});
        _view.OnFinishLoading += (_, _, _) =>
        {
            _isLoaded = true;
            _view!.Focus();
        };
        _view.HTML = "<html><head><style type='text/css'>*,html,body{margin:0;padding:0;width:auto;height:auto;font-family:'Ubuntu Regular'}input{padding:5px;}</style></head><body><input type='text'/></body></html>";
    }

    public override void Update(Time time)
    {
        _renderer!.Update();

        if (_isLoaded)
        {
            _renderer!.Render();
        }
        else return;
        
        World.Query(in _query, (ref UiComponent ui) =>
        {
            var bmp = _view!.Surface!.Value.Bitmap;
            var bytes = new byte[bmp.Width * bmp.Height * bmp.Bpp];
            if (ui.Sprite == null)
            {
                ui.Sprite = new Sprite(new Texture(_view.Width, _view.Height));
            }
            unsafe
            {
                Marshal.Copy((IntPtr)bmp.LockPixels(), bytes, 0, bytes.Length);
            }
            bmp.UnlockPixels();
            ui.Sprite!.Texture.Update(bytes);

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                _view!.FireMouseEvent(new ULMouseEvent(){Button = ULMouseEventButton.Left, Type = ULMouseEventType.MouseDown, X = 1, Y = 1});
            }

            _renderTarget.RenderTexture?.Draw(ui.Sprite);
        });
    }
}