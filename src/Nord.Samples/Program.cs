using Microsoft.Extensions.Hosting;
using Nord.Engine.Core;
using Nord.Engine.Core.Extensions;
using Nord.Engine.Scenes;
using Nord.Samples.HelloWorld;
using Nord.Samples.HelloWorld.Scenes;

using var host = Host.CreateDefaultBuilder(args)
    .AddNordEngine<HelloWorldApplication>(opts =>
    {
        
    })
    .AddScenes()
    .ConfigureServices((context, services) =>
    {
        // var container = context.GetRootContainer();
    })
    .Build();

host.RunApplication();