using System;

using Microsoft.Xna.Framework;

namespace KLib
{
    class ConsoleLine
    {
        public string text;
        public Color color;

        public ConsoleLine(string text, Color color = default(Color))
        {
            this.text = text;

            if (color == default(Color))
                this.color = Color.White;
            else
                this.color = color;
        }
    }
}
