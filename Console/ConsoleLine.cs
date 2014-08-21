using System;

using Microsoft.Xna.Framework;

namespace KLib
{
    class ConsoleLine
    {
        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        private Color color;
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

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
