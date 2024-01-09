namespace Nord.Engine.Core.Rendering;

public class RenderLayerRenderTarget : RenderTarget, IRenderLayerRenderTarget
{
    public int Layer { get; }

    public RenderLayerRenderTarget(int layer)
    {
        Layer = layer;
    }
}