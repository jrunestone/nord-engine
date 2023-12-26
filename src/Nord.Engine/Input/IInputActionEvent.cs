namespace Nord.Engine.Input;

public interface IInputActionEvent
{
    
}

public interface IInputActionEvent<T> : IInputActionEvent 
    where T : IInputAction
{
    T InputAction { get; }
}