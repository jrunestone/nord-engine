using Nord.Engine.Core.Bus;

namespace Nord.Engine.Input;

public interface IChangeInputActionMapCommand : ICommand
{
    Type InputActionMapType { get; }
}

public class ChangeInputActionMapCommand<T> : IChangeInputActionMapCommand where T : IInputActionMap
{
    public Type InputActionMapType => typeof(T);
}