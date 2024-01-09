using Arch.Core;
using Nord.Engine.Core;
using Nord.Engine.Core.Extensions;
using Nord.Engine.Core.Rendering;
using SFML.Graphics;

namespace Nord.Engine.Ecs.Systems;

public class DefaultRenderLayerSystem : SystemBase
{
    private readonly IMainRenderTarget _mainRenderTarget;
    private readonly IEnumerable<IRenderLayerRenderTarget> _renderLayerRenderTargets;

    public DefaultRenderLayerSystem(
        World world,
        IMainRenderTarget mainRenderTarget,
        IEnumerable<IRenderLayerRenderTarget> renderLayerRenderTargets) : base(world)
    {
        _mainRenderTarget = mainRenderTarget;
        _renderLayerRenderTargets = renderLayerRenderTargets;
    }

    public override void BeforeUpdate(Time time)
    {
        _renderLayerRenderTargets.ForEach(x => x.RenderTexture?.Clear(Color.Transparent));
    }

    public override void Update(Time time)
    {
        foreach (var renderLayer in _renderLayerRenderTargets)
        {
            renderLayer.RenderTexture?.Display();
            _mainRenderTarget.RenderTexture?.Draw(renderLayer.Sprite);
        }
    }
}