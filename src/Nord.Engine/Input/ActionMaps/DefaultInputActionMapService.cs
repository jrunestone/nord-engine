namespace Nord.Engine.Input.ActionMaps;

public class DefaultInputActionMapService : IInputActionMapService
{
    public IInputActionMap? CurrentActionMap { get; private set; }
    private readonly IEnumerable<IInputActionMap> _inputActionMaps;

    public DefaultInputActionMapService(IEnumerable<IInputActionMap> inputActionMaps)
    {
        _inputActionMaps = inputActionMaps;
    }
    
    public void SetInputActionMap<T>() where T : IInputActionMap
    {
        SetInputActionMap(typeof(T));
    }

    public void SetInputActionMap(Type inputActionMapType)
    {
        if (!typeof(IInputActionMap).IsAssignableFrom(inputActionMapType))
        {
            throw new ArgumentException($"Provided type is not of type {nameof(IInputActionMap)}");
        }
        
        CurrentActionMap = _inputActionMaps
                               .SingleOrDefault(x => x.GetType() == inputActionMapType) ??
                           throw new ArgumentException("No such action map type registered: {Type}", 
                               inputActionMapType.Name);
    }
}