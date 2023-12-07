using SFML.Graphics;

namespace Nord.Engine.Ecs.Components;

public class AnimationComponent
{
    public Animation? CurrentAnimation { get; set; }
    public float FrameCounter { get; set; }
    public required List<Animation> Animations { get; init; }
}

public class Animation
{
    public required string Name { get; init; }
    public AnimationFrame? CurrentFrame { get; set; }
    public required List<AnimationFrame> Frames { get; init; }
}

public class AnimationFrame
{
    public required IntRect Coords { get; init; }
    public float Delay { get; init; }
}