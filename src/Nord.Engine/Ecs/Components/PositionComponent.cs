namespace Nord.Engine.Ecs.Components;

public class PositionComponent(float x, float y)
{
    public float X { get; set;  } = x;
    public float Y { get; set;  } = y;
}