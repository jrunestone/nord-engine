using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using Nord.Engine.Core;
using Nord.Engine.Core.Rendering;
using Nord.Engine.Ecs.Components;

namespace Nord.Engine.Ecs.Systems;

public class DefaultRenderSystem : SystemBase
{
    private readonly IMainRenderTarget _mainRenderTarget;
    private readonly QueryDescription _query = new QueryDescription()
        .WithAny<DrawableComponent>();

    private class DrawableEntity
    {
        public required Entity Entity { get; init; }
        public required IDrawableComponent Drawable { get; init; }
    }
    
    public DefaultRenderSystem(World world, IMainRenderTarget mainRenderTarget) : base(world)
    {
        _mainRenderTarget = mainRenderTarget;
    }
    
    public override void Update(Time time)
    {
        if (_mainRenderTarget.RenderTexture == null)
        {
            return;
        }
        
        // TODO: sort only when changed?
        var entities = new List<Entity>();
        World.GetEntities(in _query, entities);

        var sortedEntities = entities
            .Where(x => GetDrawableComponent(x) != null)
            .Select(x => new DrawableEntity { Entity = x, Drawable = GetDrawableComponent(x)! })
            
            // sort by layer
            .OrderBy(GetEntityRenderLayer)
            
            // then by texture (if any)
            .ThenBy(GetEntityTextureId);
        
        foreach (var entity in sortedEntities)
        {
            _mainRenderTarget.RenderTexture.Draw(entity.Drawable.Drawable);
        }
    }

    private int GetEntityRenderLayer(DrawableEntity entity)
    {
        return entity.Entity.Has<RenderLayerComponent>() ? 
            entity.Entity.Get<RenderLayerComponent>().Layer 
            : (int)RenderLayer.Default;
    }

    private uint GetEntityTextureId(DrawableEntity entity)
    {
        return entity.Drawable.TextureId ?? 0;
    }

    private IDrawableComponent? GetDrawableComponent(Entity entity)
    {
        return entity.GetAllComponents()
            .OfType<IDrawableComponent>()
            .FirstOrDefault();
    }
}