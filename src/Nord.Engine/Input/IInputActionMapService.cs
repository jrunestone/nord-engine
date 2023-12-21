namespace Nord.Engine.Input;

public interface IInputActionMapService
{
    IInputActionMap? CurrentActionMap { get; }
    void SetInputActionMap<T>() where T : IInputActionMap;
    void SetInputActionMap(Type inputActionMapType);
}