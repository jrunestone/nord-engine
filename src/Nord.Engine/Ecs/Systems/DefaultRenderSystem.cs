using Arch.Core;
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
        World.Query(in _query, (ref SpriteComponent sprite) =>
        {
            _renderTarget.RenderTexture?.Draw(sprite.Sprite);
        });
    }
}