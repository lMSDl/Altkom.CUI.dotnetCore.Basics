using System;
using Microsoft.Extensions.Configuration;
using Core.Basics.Program.Models;

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

        public static Settings Settings {get;} = new Settings();

        static Program() {
            Config.Bind(Settings);
        }

        static void Main(string[] args)
        {
            Hello(Settings.Section.Key1, Settings.Section.Subsection.Key1);
            Hello(Config["Section:Key2"], Config["Section:Subsection:Key1"]);
        }

        private static void Hello(string what, string from)
        {
            Console.WriteLine(
                            Figgle.FiggleFonts.Standard.Render($"{what} {from}"));
        }
    }
}
