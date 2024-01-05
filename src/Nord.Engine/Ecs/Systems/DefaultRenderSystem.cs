using Arch.Core;
using Arch.Core.Extensions;
using Nord.Engine.Core;
using Nord.Engine.Ecs.Components;

namespace Nord.Engine.Ecs.Systems;

public class DefaultRenderSystem : SystemBase
{
    private readonly MainRenderTarget _renderTarget;
    private readonly QueryDescription _query = new QueryDescription()
        .WithAll<SpriteComponent>();

    public DefaultRenderSystem(World world, MainRenderTarget renderTarget) : base(world)
    {
        _renderTarget = renderTarget;
    }
    
    public override void Update(Time time)
    {
        // TODO: sort only when changed?
        var entities = new List<Entity>();
        World.GetEntities(in _query, entities);

        var sortedEntities = entities
            .OrderBy(GetEntityRenderLayer)
            .ThenBy(GetEntityTextureId);
        
        foreach (var entity in entities)
        {
            var sprite = entity.Get<SpriteComponent>();
            _renderTarget.RenderTexture?.Draw(sprite.Sprite);
        }
    }

    private int GetEntityRenderLayer(Entity entity)
    {
        return entity.Has<RenderLayerComponent>() ? 
            entity.Get<RenderLayerComponent>().Layer 
            : (int)RenderLayer.Default;
    }

    private uint GetEntityTextureId(Entity entity)
    {
        return entity.Get<SpriteComponent>().TextureId;
    }
}