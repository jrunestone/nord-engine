namespace Nord.Engine.Input.ActionMaps;

public interface IInputActionMap
{
    public IDictionary<IInputAction, IInputDeviceTrigger> InputActions { get; }
}