using System;

namespace Core.Basics.Program.Services {
    public class ConsoleWriteLineService : IConsoleWriteLineService
    {
        public void Execute(string line)
        {
            Console.WriteLine(line);
        }
    }
}