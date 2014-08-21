using System;

using Microsoft.Xna.Framework.Input;

namespace KLib
{
    class ConsoleKey
    {
        private Keys key;
        public Keys Key
        {
            get { return key; }
            set { key = value; }
        }
        private char lower;
        public char Lower
        {
            get { return lower; }
            set { lower = value; }
        }
        private char upper;
        public char Upper
        {
            get { return upper; }
            set { upper = value; }
        }

        public ConsoleKey(Keys key, char lower, char upper)
        {
            this.key = key;
            this.lower = lower;
            this.upper = upper;
        }
    }
}
