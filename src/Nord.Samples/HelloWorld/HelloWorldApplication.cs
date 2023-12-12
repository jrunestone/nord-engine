using MediatR;
using Nord.Engine.Core;
using Nord.Engine.Core.Configuration;
using Nord.Engine.Scenes;
using Nord.Samples.HelloWorld.Scenes;
using SFML.System;

namespace Nord.Samples.HelloWorld;

public class HelloWorldApplication : DefaultApplication
{
    private readonly IMediator _mediator;

    public HelloWorldApplication(
        EngineOptions options,
        Clock clock,
        MainRenderTarget mainRenderTarget,
        IMediator mediator,
        ISceneService sceneService) 
        : base(options, clock, mainRenderTarget, sceneService)
    {
        _mediator = mediator;
    }
    
    public override void Run()
    {
        _mediator.Send(new ChangeSceneRequest<MainScene>());
        base.Run();
    }
}