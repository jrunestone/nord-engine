using Arch.Core;
using Nord.Engine.Core;
using Nord.Engine.Ecs.Components;

namespace Nord.Engine.Ecs.Systems;

public class DefaultTextRenderSystem : SystemBase
{
    private readonly MainRenderTarget _renderTarget;
    private readonly QueryDescription _query = new QueryDescription()
        .WithAll<TextComponent>();

    public DefaultTextRenderSystem(
        World world,
        MainRenderTarget renderTarget) : base(world)
    {
        _renderTarget = renderTarget;
    }
    
    public override void Update(float dt)
    {
        World.Query(in _query, (ref TextComponent text) =>
        {
            _renderTarget.RenderTexture?.Draw(text.Text);
        });
    }
}