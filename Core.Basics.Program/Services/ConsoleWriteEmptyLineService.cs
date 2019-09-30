using System;

namespace Core.Basics.Program.Services {
    public class ConsoleWriteEmptyLineService : IConsoleWriteLineService
    {
        public void Execute(string line)
        {
            Console.WriteLine("empty");
        }
    }
}