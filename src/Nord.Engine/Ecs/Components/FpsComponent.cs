namespace Nord.Engine.Ecs.Components;

public class FpsComponent
{
    public double ElapsedTime { get; set; }
    public double FrameCounter { get; set; }
    public int AverageFps { get; set; }
}