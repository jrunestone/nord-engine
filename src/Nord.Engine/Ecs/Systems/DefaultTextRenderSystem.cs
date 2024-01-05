using Arch.Core;
using Nord.Engine.Core;
using Nord.Engine.Ecs.Components;

namespace Nord.Engine.Ecs.Systems;

public class DefaultTextRenderSystem(
    World world,
    MainRenderTarget renderTarget) : SystemBase(world)
{
    private readonly MainRenderTarget _renderTarget = renderTarget;
    private readonly QueryDescription _query = new QueryDescription()
        .WithAll<TextComponent>();
    
    public override void Update(Time time)
    {
        World.Query(in _query, (ref TextComponent text) =>
        {
            _renderTarget.RenderTexture?.Draw(text.Text);
        });
    }
}