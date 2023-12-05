using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nord.Engine.Core.Assets;
using Nord.Engine.Core.Configuration;
using Nord.Engine.Ecs;
using Nord.Engine.Ecs.Systems;
using Nord.Engine.Scenes;
using Serilog;
using SFML.Graphics;
using SFML.System;
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
            {
                // this creates new singletons in child containers = not same instance anymore
                // cfg.WithReBuildSingletonsInChildContainer() 
            });

        hostBuilder.Properties[nameof(StashboxContainer)] = container;
        
        return hostBuilder
            .UseStashbox(container)
            .ConfigureAppConfiguration(cfg =>
            {
                cfg.AddJsonFile("appsettings.json");
            })
            .ConfigureServices((context, services) =>
            {
                // options
                AddOptions(context, services, engineConfiguration);
                
                // core
                services.AddSingleton<Clock>(_ => new Clock());
                services.AddSingleton<ITextureCache, DefaultTextureCache>();
                services.AddSingleton<IFontCache, DefaultFontCache>();
                services.AddSingleton<MainRenderTarget>();

                // scenes
                services.AddSingleton<ISceneFactory, DefaultSceneFactory>(_ => new DefaultSceneFactory(container));
                services.AddSingleton<ISceneService, DefaultSceneService>();

                // main application
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
        container.RegisterOpenGenericImplementations(Assembly.GetCallingAssembly(), typeof(ISceneCompositionRoot<>));
        return hostBuilder;
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
    
    public static void RunApplication(this IHost host)
    {
        var scope = host.Services.CreateScope();
        var app = scope.ServiceProvider.GetRequiredService<IApplication>();
        app.Run();
    }
    
    private static IServiceCollection AddOptions(
        HostBuilderContext context, 
        IServiceCollection services,
        Action<EngineOptions>? engineConfiguration = null)
    {
        services.Configure<EngineOptions>(context.Configuration.GetSection(EngineOptions.SectionName));

        services.AddSingleton<EngineOptions>(_ =>
        {
            var opts = new EngineOptions();
            context.Configuration.GetSection(EngineOptions.SectionName).Bind(opts);
            engineConfiguration?.Invoke(opts);

            // set dynamic options
            opts.AssetRootPath = Path.Combine(context.HostingEnvironment.ContentRootPath, opts.AssetRootPath ?? string.Empty);
            
            return opts;
        });

        return services;
    }
}