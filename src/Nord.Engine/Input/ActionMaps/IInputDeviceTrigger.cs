namespace Nord.Engine.Input.ActionMaps;

public interface IInputDeviceTrigger
{
    bool IsActive { get; }
    bool IsActivated { get; }

    void Update();
}