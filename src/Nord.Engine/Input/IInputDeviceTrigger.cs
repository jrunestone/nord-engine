namespace Nord.Engine.Input;

public interface IInputDeviceTrigger
{
    bool IsActive { get; }
    bool IsActivated { get; }

    void Update();
}