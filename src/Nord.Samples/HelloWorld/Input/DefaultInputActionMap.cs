using Nord.Engine.Input;
using Nord.Engine.Input.ActionMaps;
using SFML.Window;

namespace Nord.Samples.HelloWorld.Input;

public class DefaultInputActionMap : IInputActionMap
{
    public IDictionary<IInputAction, IInputDeviceTrigger> InputActions { get; } =
        new Dictionary<IInputAction, IInputDeviceTrigger>
        {
            { new RightInputAction(), new KeyboardDeviceTrigger(Keyboard.Key.Right) }
        };
}