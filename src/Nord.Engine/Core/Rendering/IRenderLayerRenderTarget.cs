namespace Nord.Engine.Core.Rendering;

public interface IRenderLayerRenderTarget : IRenderTarget
{
    int Layer { get; }
}