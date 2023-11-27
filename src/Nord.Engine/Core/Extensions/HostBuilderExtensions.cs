using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nord.Engine.Core.Configuration;
using Nord.Engine.Scenes;
using Serilog;
using Stashbox;

namespace Nord.Engine.Core.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder AddNordEngine<TApplication>(
        this IHostBuilder hostBuilder, 
        Action<EngineOptions>? engineConfiguration = null) 
        where TApplication : class, IApplication
    {
        var container = new StashboxContainer(cfg =>
            cfg.WithReBuildSingletonsInChildContainer());

        hostBuilder.Properties[nameof(StashboxContainer)] = container;
        
        return hostBuilder
            .UseStashbox(container)
            .ConfigureAppConfiguration(cfg =>
            {
                cfg.AddJsonFile("appsettings.json");
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<EngineOptions>(context.Configuration.GetSection(EngineOptions.SectionName));

                services.AddSingleton<EngineOptions>(_ =>
                {
                    var opts = new EngineOptions();
                    context.Configuration.GetSection(EngineOptions.SectionName).Bind(opts);
                    engineConfiguration?.Invoke(opts);
                    // TODO: validate options object
                    return opts;
                });

                services.AddSingleton<ISceneFactory, DefaultSceneFactory>(_ => new DefaultSceneFactory(container));
                services.AddSingleton<ISceneService, DefaultSceneService>();

                services.AddScoped<IApplication, TApplication>();
            })
            .UseSerilog((hostingContext, services, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom
                    .Configuration(hostingContext.Configuration);
            });
    }

    public static IHostBuilder AddScenes(this IHostBuilder hostBuilder)
    {
        var container = hostBuilder.GetRootContainer();
        container.RegisterImplementations<IScene>(Assembly.GetCallingAssembly());
        return hostBuilder;
    }
    
    public static void RunApplication(this IHost host)
    {
        var scope = host.Services.CreateScope();
        var app = scope.ServiceProvider.GetRequiredService<IApplication>();
        app.Run();
    }
    
    public static IStashboxContainer GetRootContainer(this IHostBuilder hostBuilder)
    {
        return hostBuilder.Properties[nameof(StashboxContainer)] as IStashboxContainer ??
               throw new InvalidOperationException("Root container hasn't been registered yet");
    }
    
    public static IStashboxContainer GetRootContainer(this HostBuilderContext hostBuilderContext)
    {
        return hostBuilderContext.Properties[nameof(StashboxContainer)] as IStashboxContainer ??
               throw new InvalidOperationException("Root container hasn't been registered yet");
    }
}