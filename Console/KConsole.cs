using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KLib
{
    public enum ConsoleLogLevel
    {
        Normal,
        Internal,
        Help,
        Message,
        Warning,
        Error
    }

    public static class KConsole
    {
        public static bool visible = false;
        public static bool active = false;
        public static List<ConsoleCommand> commands = new List<ConsoleCommand>();

        static SpriteFont font = null;
        static string line = string.Empty;
        static List<ConsoleLine> history = new List<ConsoleLine>();
        static int offset = 0;
        static int maxLines = 10;
        static float height = 0;
        static FloatTween heightTween = Tween.AddFloat();

        static List<ConsoleKey> keys = new List<ConsoleKey>()
        {
            // Letters
            new ConsoleKey(Keys.A, 'a', 'A'),
            new ConsoleKey(Keys.B, 'b', 'B'),
            new ConsoleKey(Keys.C, 'c', 'C'),
            new ConsoleKey(Keys.D, 'd', 'D'),
            new ConsoleKey(Keys.E, 'e', 'E'),
            new ConsoleKey(Keys.F, 'f', 'F'),
            new ConsoleKey(Keys.G, 'g', 'G'),
            new ConsoleKey(Keys.H, 'h', 'H'),
            new ConsoleKey(Keys.I, 'i', 'I'),
            new ConsoleKey(Keys.J, 'j', 'J'),
            new ConsoleKey(Keys.K, 'k', 'K'),
            new ConsoleKey(Keys.L, 'l', 'L'),
            new ConsoleKey(Keys.M, 'm', 'M'),
            new ConsoleKey(Keys.N, 'n', 'N'),
            new ConsoleKey(Keys.O, 'o', 'O'),
            new ConsoleKey(Keys.P, 'p', 'P'),
            new ConsoleKey(Keys.Q, 'q', 'Q'),
            new ConsoleKey(Keys.R, 'r', 'R'),
            new ConsoleKey(Keys.S, 's', 'S'),
            new ConsoleKey(Keys.T, 't', 'T'),
            new ConsoleKey(Keys.U, 'u', 'U'),
            new ConsoleKey(Keys.V, 'v', 'V'),
            new ConsoleKey(Keys.W, 'w', 'W'),
            new ConsoleKey(Keys.X, 'x', 'X'),
            new ConsoleKey(Keys.Y, 'y', 'Y'),
            new ConsoleKey(Keys.Z, 'z', 'Z'),

            // Numbers
            new ConsoleKey(Keys.D1, '1', '!'),
            new ConsoleKey(Keys.D2, '2', '@'),
            new ConsoleKey(Keys.D3, '3', '#'),
            new ConsoleKey(Keys.D4, '4', '$'),
            new ConsoleKey(Keys.D5, '5', '%'),
            new ConsoleKey(Keys.D6, '6', '^'),
            new ConsoleKey(Keys.D7, '7', '&'),
            new ConsoleKey(Keys.D8, '8', '*'),
            new ConsoleKey(Keys.D9, '9', '('),
            new ConsoleKey(Keys.D0, '0', ')'),

            // Symbols
            new ConsoleKey(Keys.Space, ' ', ' '),
            new ConsoleKey(Keys.OemMinus, '-', '_'),
            new ConsoleKey(Keys.OemPlus, '=', '+'),
            new ConsoleKey(Keys.OemOpenBrackets, '[', '{'),
            new ConsoleKey(Keys.OemCloseBrackets, ']', '}'),
            new ConsoleKey(Keys.OemBackslash, '\\', '|'),
            new ConsoleKey(Keys.OemSemicolon, ';', ':'),
            new ConsoleKey(Keys.OemQuotes, '\'', '"'),
            new ConsoleKey(Keys.OemComma, ',', '<'),
            new ConsoleKey(Keys.OemPeriod, '.', '>'),
            new ConsoleKey(Keys.OemQuestion, '/', '?'),
        };

        public static void Init()
        {
            // Initialize Font
            font = Engine.content.Load<SpriteFont>(@"Fonts\ConsoleFont");

            // Help Console Command
            ConsoleCommand helpCommand = new ConsoleCommand(Help, "help");
            helpCommand.help = "Shows help for all commands";
            AddCommand(helpCommand);

            // Exit/Quit Console Command
            ConsoleCommand quitCommand = new ConsoleCommand(Quit, "quit", "exit");
            quitCommand.help = "Quit game";
            AddCommand(quitCommand);

            // Error Console Command
            ConsoleCommand errorCommand = new ConsoleCommand(Error, "error");
            errorCommand.help = "Throw a test Exception";
            AddCommand(errorCommand);
        }

        public static void AddCommand(ConsoleCommand command)
        {
            commands.Add(command);
        }

        public static void Log(string text, ConsoleLogLevel logLevel = ConsoleLogLevel.Normal)
        {
            List<ConsoleLine> lines = new List<ConsoleLine>();
            lines.Add(new ConsoleLine(text));

            // Line splitting
            int split = 0;
            float bounds = 24f;
            bool firstTrimmed = false;
            if (Engine.screenWidth > 0)
                for (int i = 0; i < text.Length; i++)
                {
                    bounds += font.MeasureString(text.Substring(i, 1)).X;

                    if (bounds > Engine.screenWidth)
                    {
                        if (!firstTrimmed)
                        {
                            lines[0].text = lines[0].text.Substring(0, i);
                            firstTrimmed = true;
                        }

                        split = i;
                        bounds = 0;

                        string sub = text.Substring(split, text.Length - i);
                        lines.Add(new ConsoleLine(sub));
                    }
                }

            // Set color
            foreach (ConsoleLine line in lines)
                switch (logLevel)
                {
                    case ConsoleLogLevel.Normal:
                        line.color = Color.White;
                        break;
                    case ConsoleLogLevel.Internal:
                        line.color = Color.SlateBlue;
                        break;
                    case ConsoleLogLevel.Help:
                        line.color = Color.LightBlue;
                        break;
                    case ConsoleLogLevel.Message:
                        line.color = Color.LimeGreen;
                        break;
                    case ConsoleLogLevel.Warning:
                        line.color = Color.Yellow;
                        break;
                    case ConsoleLogLevel.Error:
                        line.color = Color.Red;
                        break;
                }

            // Add to history
            foreach (ConsoleLine line in lines)
                history.Add(line);
        }

        public static void Toggle()
        {
            if (heightTween.State == TweenState.Running)
                return;

            if (visible)
                heightTween.Start(Engine.screenHeight / 2, 0, 0.5f, ScaleFuncs.Linear);
            else
                heightTween.Start(0, Engine.screenHeight / 2, 0.5f, ScaleFuncs.Linear);
        }

        public static void Draw(SpriteBatch batch)
        {
            if (!visible)
                return;

            Shape.DrawRect(0, 0, Engine.screenWidth, (int)height, Color.Gray, new Color(0, 0, 0, 128));

            if (active)
            {
                // Offset
                if (history.Count > maxLines)
                    offset = history.Count - maxLines;

                // Lines
                if (history.Count > 0)
                    for (int i = offset; i < history.Count; i++)
                        Text.DrawText(font, history[i].text, new Vector2(12, 10 + ((i - offset) * 20)), history[i].color);

                // Prompt
                Text.DrawText(font, ">", new Vector2(12, (Engine.screenHeight / 2) - 24), Color.White);
                Text.DrawText(font, line, new Vector2(24, (Engine.screenHeight / 2) - 24), Color.White);

                // Cursor
                Vector2 lineSize = font.MeasureString(line);
                float color = 0.75f + Utils.SineWave(8, 0.25f);
                Text.DrawText(font, "|", new Vector2(24 + lineSize.X, (Engine.screenHeight / 2) - 24), new Color(color, color, color));
            }
        }

        public static void Update(GameTime dt)
        {
            visible = !(height <= 0);
            active = (height >= Engine.screenHeight / 2);

            height = heightTween.CurrentValue;

            maxLines = ((Engine.screenHeight / 2) / 20 - 2);
        }

        public static void CheckInput(GameTime gt)
        {
            if (active)
            {
                // Keys
                foreach (ConsoleKey key in keys)
                {
                    if (Input.KeyPressed(key.key))
                    {
                        if (Input.IsKeyDown(Keys.LeftShift))
                            line += key.upper;
                        else
                            line += key.lower;
                    }
                }

                // Backspace
                if (Input.KeyPressed(Keys.Back))
                    if (line.Length > 0)
                        line = line.Remove(line.Length - 1);

                // TODO: Page Up/Down to scroll history

                // Run Command
                if (Input.KeyPressed(Keys.Enter) && line != string.Empty)
                {
                    // Add command to history
                    Log(">" + line);

                    // Iterate commands
                    foreach (ConsoleCommand command in commands)
                        foreach (string name in command.names)
                            if (line.ToLower() == name.ToLower())
                            {
                                string full = line;
                                string[] args = full.Split(' ');

                                command.Run(args, args.Length, full);
                            }

                    // Clear prompt    
                    line = string.Empty;
                }
            }
        }

        public static void Help(string[] args, int length, string full)
        {
            Log("Command Help", ConsoleLogLevel.Help);
            Log("--------------------------------------------------", ConsoleLogLevel.Help);

            foreach (ConsoleCommand command in commands)
            {
                if (command.help != string.Empty)
                {
                    string help = string.Empty;

                    // Name
                    help += command.names[0];

                    // Arguments
                    if (command.arguments.Count > 0)
                        foreach (ConsoleCommandArgument argument in command.arguments)
                            if (argument.optional)
                                help += " [" + argument.name + "]";
                            else
                                help += "<" + argument.name + ">";

                    // Seperator
                    help += " - ";

                    // Help
                    help += command.help + " ";

                    // Aliases
                    if (command.names.Count > 1)
                    {
                        help += "[";

                        for (int i = 1; i < command.names.Count; i++)
                            if (i == command.names.Count - 1)
                                help += command.names[i];
                            else
                                help += command.names[i] + ", ";

                        help += "]";
                    }

                    // Log
                    Log(help, ConsoleLogLevel.Help);
                }
            }
            
            Log("--------------------------------------------------", ConsoleLogLevel.Help);
        }

        public static void Quit(string[] args, int length, string full)
        {
            Environment.Exit(0);
        }
        public static void Error(string[] args, int length, string full)
        {
            throw new Exception();
        }
    }
}
