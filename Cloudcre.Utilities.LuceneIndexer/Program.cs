using System.Collections.Generic;
using System.Linq;
using ManyConsole;

namespace Cloudcre.Utilities.LuceneIndexer
{
    class Program
    {
        static void Main(string[] args)
        {
            // locate any commands in the assembly (or use an IoC container, or whatever source)
            var commands = GetCommands();

            // optionally, include ConsoleModeCommand if you want to allow the user to run
            // commands from the console.
            var consoleRunner = new ConsoleModeCommand(GetCommands);
            commands = commands.Concat(new[] { consoleRunner });

            // run the command for the console input
            ConsoleCommandDispatcher.DispatchCommand(commands, args, System.Console.Out);
        }

        static IEnumerable<ConsoleCommand> GetCommands()
        {
            return ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program));
        }
    }
}