using System;

using Microsoft.Xna.Framework.Input;

namespace KLib
{
    class ConsoleKey
    {
        public Keys key;
        public char lower;
        public char upper;

        public ConsoleKey(Keys key, char lower, char upper)
        {
            this.key = key;
            this.lower = lower;
            this.upper = upper;
        }
    }
}
