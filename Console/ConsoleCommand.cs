using System;
using System.Collections.Generic;
using System.IO;

namespace KLib
{
    public delegate void ConsoleCommandDelegate(string[] args, int length, string full);

    public class ConsoleCommandArgument
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private bool optional;
        public bool Optional
        {
            get { return optional; }
            set { optional = value; }
        }

        public ConsoleCommandArgument(string name, bool optional)
        {
            this.name = name;
            this.optional = optional;
        }
    }

    public class ConsoleCommand
    {
        private ConsoleCommandDelegate command;
        public ConsoleCommandDelegate Command
        {
            get { return command; }
            set { command = value; }
        }
        private List<string> names;
        public List<string> Names
        {
            get { return names; }
            set { names = value; }
        }
        private List<ConsoleCommandArgument> arguments;
        public List<ConsoleCommandArgument> Arguments
        {
            get { return arguments; }
            set { arguments = value; }
        }
        private string help;
        public string Help
        {
            get { return help; }
            set { help = value; }
        }

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
