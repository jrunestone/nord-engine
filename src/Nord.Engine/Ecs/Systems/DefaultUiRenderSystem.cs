using System.Diagnostics;
using System.Runtime.InteropServices;
using Arch.Core;
using Nord.Engine.Core;
using Nord.Engine.Ecs.Components;
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
    
    private readonly MainRenderTarget _renderTarget;
    private readonly QueryDescription _query = new QueryDescription()
        .WithAll<UiComponent>();

    public DefaultUiRenderSystem(
        World world,
        MainRenderTarget renderTarget) : base(world)
    {
        _renderTarget = renderTarget;
    }

    public override void Initialize()
    {
        AppCoreMethods.SetPlatformFontLoader();
        var cfg = new ULConfig();
        _renderer = ULPlatform.CreateRenderer(cfg);
        _view = _renderer.CreateView(1024, 768);
        _view.OnFinishLoading += (_, _, _) =>
        {
            _isLoaded = true;
            _view!.Focus();
        };
        _view.HTML = "<html><head><style type='text/css'>*,html,body{margin:0;padding:0;}</style></head><body><input type='text'/></body></html>";
    }

    public override void Update(float dt)
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

            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                _view!.FireKeyEvent(ULKeyEvent.Create(ULKeyEventType.RawKeyDown, ULKeyEventModifiers.ShiftKey, 65, 0, "A", "a", false, false, false));
                _view!.FireKeyEvent(ULKeyEvent.Create(ULKeyEventType.Char, ULKeyEventModifiers.ShiftKey, 65, 0, "A", "a", false, false, false));
            }
            _renderTarget.RenderTexture?.Draw(ui.Sprite);
        });
    }
}