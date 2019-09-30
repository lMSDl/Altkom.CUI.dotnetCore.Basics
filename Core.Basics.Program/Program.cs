using System;
using Microsoft.Extensions.Configuration;

namespace Core.Basics.Program
{
    class Program
    {
        public static IConfiguration Config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddXmlFile("appsettings.xml", optional: true, reloadOnChange: true)
            .AddIniFile("appsettings.ini", optional: true, reloadOnChange: true)
            .AddYamlFile("appsettings.yaml", optional: true, reloadOnChange: true)
            .Build();

        static void Main(string[] args)
        {
            Hello(Config["HelloJson"]);
            Hello(Config["HelloXml"]);
            Hello(Config["HelloYaml"]);
            Hello(Config["HelloIni"]);
        }

        private static void Hello(string from)
        {
            Console.WriteLine(
                            Figgle.FiggleFonts.Standard.Render($"Hello {from}"));
        }
    }
}
