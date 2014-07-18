using System;

using Microsoft.Xna.Framework;

namespace KLib
{
    public class TooltipLine
    {
        public string text;
        public Color color;

        public TooltipLine(string text, Color color)
        {
            this.text = text;
            this.color = color;
        }
    }
}
