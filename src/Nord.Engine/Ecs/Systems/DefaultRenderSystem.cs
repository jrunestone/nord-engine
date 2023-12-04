using Arch.Core;
using Nord.Engine.Core;
using Nord.Engine.Ecs.Components;

namespace Nord.Engine.Ecs.Systems;

public class DefaultRenderSystem(
    World world,
    MainRenderTarget renderTarget) : SystemBase(world)
{
    private readonly MainRenderTarget _renderTarget = renderTarget;
    private readonly QueryDescription _query = new QueryDescription()
        .WithAll<SpriteComponent>();
    
    public override void Update(float dt)
    {
        World.Query(in _query, (ref SpriteComponent sprite) =>
        {
            _renderTarget.RenderTarget?.Draw(sprite.Sprite);

        });
    }
}