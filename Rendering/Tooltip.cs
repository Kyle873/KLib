using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public class TooltipLine
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

        public TooltipLine(string text, Color color)
        {
            this.text = text;
            this.color = color;
        }
    }

    public class Tooltip
    {
        public const char NewLine = '~';

        private List<TooltipLine> lines = new List<TooltipLine>();
        public List<TooltipLine> Lines
        {
            get { return lines; }
            set { lines = value; }
        }
        private bool frame = false;
        public bool Frame
        {
            get { return frame; }
            set { frame = value; }
        }
        private Color frameColor = new Color(128, 128, 128, 192);
        public Color FrameColor
        {
            get { return frameColor; }
            set { frameColor = value; }
        }
        private Color frameFillColor = new Color(32, 32, 32, 128);
        public Color FrameFillColor
        {
            get { return frameFillColor; }
            set { frameFillColor = value; }
        }

        public void Draw(SpriteBatch batch, SpriteFont font)
        {
            Vector2 position = new Vector2(Input.MouseX + 12, Input.MouseY + 12);
            Vector2 bounds = Vector2.Zero;
            List<int> tooltipLengths = new List<int>();
            int biggest = 0;
            int biggestIndex = 0;
            int maxLines = 0;

            // Get lengths
            for (int i = 0; i < lines.Count; i++)
                tooltipLengths.Add(lines[i].Text.Length);

            // Find biggest length
            biggest = tooltipLengths.Max();

            // Find out how many lines the tooltip has
            for (int i = 0; i < lines.Count; i++)
                if (lines[i].Text != string.Empty)
                    maxLines++;

            // Find the index of the largest line
            for (int i = 0; i < lines.Count; i++)
                if (lines[i].Text.Length == biggest)
                {
                    biggestIndex = i;
                    break;
                }

            // Measure the longest tooltip to get X bounds
            bounds = font.MeasureString(lines[biggestIndex].Text);

            // Adjust bounds to frame
            if (frame)
            {
                bounds.X += 8;
                /* Why was this here again?
                if (lines.Count > 1)
                    bounds.Y += 2; */
            }

            // Check bounds
            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > Engine.ScreenWidth - bounds.X)
                position.X = Engine.ScreenWidth - bounds.X;
            if (position.Y > Engine.ScreenHeight - bounds.Y - ((maxLines - 1) * 20))
                position.Y = Engine.ScreenHeight - bounds.Y - ((maxLines - 1) * 20);

            // Draw frame
            if (frame)
                Shape.DrawRect((int)position.X - 4, (int)position.Y - 4, (int)bounds.X + 4, (int)(bounds.Y * (lines.Count > 1 ? lines.Count : 1)) + 4, frameColor, frameFillColor);

            // Draw tooltip
            for (int i = 0; i < lines.Count; i++)
                if (lines[i].Text != string.Empty)
                {
                    if (!lines[i].Text.Contains(NewLine))
                        Text.DrawText(font, lines[i].Text, position, lines[i].Color, true);
                    
                    position.Y += 20;
                }
        }
    }
}
