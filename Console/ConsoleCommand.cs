using System;
using System.Collections.Generic;
using System.IO;

namespace KLib
{
    public delegate void ConsoleCommandDelegate(string[] args, int length, string full);

    public class ConsoleCommandArgument
    {
        public string name;
        public bool optional;

        public ConsoleCommandArgument(string name, bool optional)
        {
            this.name = name;
            this.optional = optional;
        }
    }

    public class ConsoleCommand
    {
        public ConsoleCommandDelegate command;
        public List<string> names;
        public List<ConsoleCommandArgument> arguments;
        public string help;

        public ConsoleCommand(ConsoleCommandDelegate cmd, params string[] cmdNames)
        {
            if (cmdNames == null || cmdNames.Length < 1)
                throw new NotSupportedException();

            command = cmd;
            names = new List<string>(cmdNames);
            arguments = new List<ConsoleCommandArgument>();
        }

        public bool Run(string[] args, int length, string full)
        {
            try
            {
                // File.AppendAllText(path + "Commands.log", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ": " + full + "\n");
                command(args, length, full);
            }
            catch (IndexOutOfRangeException e)
            {
                Utils.DumpError(e, "Invalid parameter");
                return false;
            }
            catch (Exception e)
            {
                Utils.DumpError(e, "Error in command: " + full);
                return false;
            }

            return true;
        }
    }
}
