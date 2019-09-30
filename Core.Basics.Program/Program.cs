using System;
using Microsoft.Extensions.Configuration;
using Core.Basics.Program.Models;
using Core.Basics.Program.Services;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using Microsoft.Extensions.Logging;

namespace Core.Basics.Program
{
    class Program
    {
        public static IConfiguration Config {get; } = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //.AddXmlFile("appsettings.xml", optional: true, reloadOnChange: true)
            //.AddIniFile("appsettings.ini", optional: true, reloadOnChange: true)
            //.AddYamlFile("appsettings.yaml", optional: true, reloadOnChange: true)
            .Build();

        public static IServiceProvider ServiceProvider {get;}

        public static Settings Settings {get;} = new Settings();

        static Program() {
            var serviceCollection = new ServiceCollection()
            .AddLogging(builder => builder
            .AddConsole()
            //.AddConsole(x => x.IncludeScopes = true)
            .AddDebug()
            .AddConfiguration(Config.GetSection("Logging")));
            //.Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Trace);
            
            var container = new Container();
            container.Configure(configureExpression => {
                configureExpression.Scan(x =>
                {
                x.AssemblyContainingType(typeof(IConsoleWriteLineService));
                x.WithDefaultConventions();
                x.AddAllTypesOf<IConsoleWriteLineService>();
                });
                configureExpression.Populate(serviceCollection);
            });

            ServiceProvider = container.GetInstance<IServiceProvider>();

            var logger = ServiceProvider.GetService<ILogger<Program>>();
            logger.LogTrace(nameof(Program));

            logger.LogDebug("Bindowanie ustawień");
            Config.Bind(Settings);
        }

        static void Main(string[] args)
        {
            var logger = ServiceProvider.GetService<ILogger<Program>>();
            logger.LogTrace(nameof(Main));

            foreach(var service in ServiceProvider.GetServices<IConsoleWriteLineService>()) {
                using(logger.BeginScope($"Wyświetlanie wiadomości z serwisu {service.GetType().Name}"))
                {
                    logger.LogDebug("Serwis wykonuje");
                    service.Execute($"{Settings.Section.Key1} {Settings.Section.Subsection.Key1}");
                    logger.LogDebug("Serwis wykonał");
                }
            }

            
                logger.LogDebug("Zakończenie");
                Console.ReadKey();
        }
    }
}
