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
        public static ILogger Logger {get;}

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

            Logger = ServiceProvider.GetService<ILogger<Program>>();
            Logger.LogTrace(nameof(Program));

            Logger.LogDebug("Bindowanie ustawień");
            Config.Bind(Settings);
        }

        static void Main(string[] args)
        {
            Logger.LogTrace(nameof(Main));

            System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionEventHandler;


            foreach(var service in ServiceProvider.GetServices<IConsoleWriteLineService>()) {
                using(Logger.BeginScope($"Wyświetlanie wiadomości z serwisu {service.GetType().Name}"))
                {
                    if(service is ConsoleWriteEmptyLineService)
                        throw new Exception($"{service.GetType().Name} failed");

                    Logger.LogDebug("Serwis wykonuje");
                    service.Execute($"{Settings.Section.Key1} {Settings.Section.Subsection.Key1}");
                    Logger.LogDebug("Serwis wykonał");
                }
            }
           
                Logger.LogDebug("Zakończenie");
                Console.ReadKey();
        }

        static void UnhandledExceptionEventHandler(object sender, UnhandledExceptionEventArgs args) {
            Logger.LogError("Błąd");
            Console.ReadKey();
            Environment.Exit(1);
        }
    }
}
