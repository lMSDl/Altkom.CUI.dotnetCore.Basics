using System;
using Microsoft.Extensions.Configuration;

namespace Core.Basics.Program
{
    class Program
    {
        public static IConfiguration Config = new ConfigurationBuilder()
            //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddXmlFile("appsettings.xml", optional: true, reloadOnChange: true)
            //.AddIniFile("appsettings.ini", optional: true, reloadOnChange: true)
            //.AddYamlFile("appsettings.yaml", optional: true, reloadOnChange: true)
            .Build();

        static void Main(string[] args)
        {
            Hello(Config["Section:Key2"], Config["Section:Subsection:Key1"]);
        }

        private static void Hello(string what, string from)
        {
            Console.WriteLine(
                            Figgle.FiggleFonts.Standard.Render($"{what} {from}"));
        }
    }
}
