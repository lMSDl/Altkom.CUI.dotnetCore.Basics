using System;

namespace Core.Basics.Program.Services {
    public class ConsoleWriteFiggleLineService : IConsoleWriteLineService
    {
        public void Execute(string line)
        {
            Console.WriteLine(Figgle.FiggleFonts.Standard.Render(line));
        }
    }
}