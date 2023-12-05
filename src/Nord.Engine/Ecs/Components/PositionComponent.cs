namespace Nord.Engine.Ecs.Components;

public class PositionComponent
{
    public float X { get; set; }
    public float Y { get; set; }

    public PositionComponent()
    {
        
    }
    
    public PositionComponent(float x, float y)
    {
        X = x;
        Y = y;
    }
}