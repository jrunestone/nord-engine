using Microsoft.Extensions.Hosting;
using Nord.Engine.Core.Extensions;
using Nord.Samples.HelloWorld;

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