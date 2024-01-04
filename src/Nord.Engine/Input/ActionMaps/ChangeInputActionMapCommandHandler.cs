using Nord.Engine.Core.Bus;

namespace Nord.Engine.Input.ActionMaps;

public class ChangeInputActionMapCommandHandler : ICommandHandler<IChangeInputActionMapCommand>
{
    private readonly IInputActionMapService _inputActionMapService;

    public ChangeInputActionMapCommandHandler(IInputActionMapService inputActionMapService)
    {
        _inputActionMapService = inputActionMapService;
    }
    
    public void Handle(IChangeInputActionMapCommand command)
    {
        _inputActionMapService.SetInputActionMap(command.InputActionMapType);
    }
}