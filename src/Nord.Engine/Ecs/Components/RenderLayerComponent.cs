namespace Nord.Engine.Ecs.Components;

public class RenderLayerComponent
{
    public int Layer { get; }

    public RenderLayerComponent(int layer)
    {
        Layer = layer;
    }
}