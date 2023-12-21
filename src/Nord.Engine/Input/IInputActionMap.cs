namespace Nord.Engine.Input;

public interface IInputActionMap
{
    public IDictionary<IInputAction, IInputDeviceTrigger> InputActions { get; }
}