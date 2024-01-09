using Arch.Core;
using Arch.Core.Extensions;
using Nord.Engine.Core.Extensions;
using Nord.Engine.Core.Rendering;
using Nord.Engine.Ecs.Components;
using Time = Nord.Engine.Core.Time;

namespace Nord.Engine.Ecs.Systems;

public class DefaultRenderSystem : SystemBase
{
    private readonly IEnumerable<IRenderLayerRenderTarget> _renderLayerRenderTargets;

    private readonly QueryDescription _query = new QueryDescription()
        .WithAny<DrawableComponent>();

    private class DrawableEntity
    {
        public required Entity Entity { get; init; }
        public required IDrawableComponent Drawable { get; init; }
        public required IRenderLayerRenderTarget RenderLayer { get; init; }
    }
    
    public DefaultRenderSystem(
        World world, 
        IEnumerable<IRenderLayerRenderTarget> renderLayerRenderTargets) : base(world)
    {
        _renderLayerRenderTargets = renderLayerRenderTargets;
    }
    
    public override void Update(Time time)
    {
        var entities = new List<Entity>();
        World.GetEntities(in _query, entities);

        var sortedEntities = entities
            .Where(x => GetDrawableComponent(x) != null)
            .Select(x => new DrawableEntity
            {
                Entity = x, 
                Drawable = GetDrawableComponent(x)!,
                RenderLayer = GetEntityRenderLayer(x)
            })
            
            // sort by layer
            .OrderBy(x => x.RenderLayer.Layer)
            
            // then by texture (if any)
            .ThenBy(x => x.Drawable.TextureId ?? 0);

        sortedEntities.ForEach(x => x.RenderLayer.RenderTexture?.Draw(x.Drawable.Drawable));
    }

    private IRenderLayerRenderTarget GetEntityRenderLayer(Entity entity)
    {
        var layerNum = entity.Has<RenderLayerComponent>() ? 
            entity.Get<RenderLayerComponent>().Layer 
            : (int)RenderLayer.Default;

        return _renderLayerRenderTargets
            .Single(x => x.Layer == layerNum);
    }

    private IDrawableComponent? GetDrawableComponent(Entity entity)
    {
        return entity.GetAllComponents()
            .OfType<IDrawableComponent>()
            .FirstOrDefault();
    }
}