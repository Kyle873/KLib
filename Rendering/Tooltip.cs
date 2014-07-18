using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public class Tooltip
    {
        public const string NewLine = "~";

        public List<TooltipLine> lines = new List<TooltipLine>();
        public bool frame = false;
        public Color frameColor = new Color(128, 128, 128, 192);
        public Color frameFillColor = new Color(32, 32, 32, 128);

        public void Draw(SpriteBatch batch, SpriteFont font)
        {
            Vector2 position = new Vector2(Input.mouseX + 12, Input.mouseY + 12);
            Vector2 bounds = Vector2.Zero;
            List<int> tooltipLengths = new List<int>();
            int biggest = 0;
            int biggestIndex = 0;
            int maxLines = 0;

            // Get lengths
            for (int i = 0; i < lines.Count; i++)
                tooltipLengths.Add(lines[i].text.Length);

            // Find biggest length
            biggest = tooltipLengths.Max();

            // Find out how many lines the tooltip has
            for (int i = 0; i < lines.Count; i++)
                if (lines[i].text != string.Empty)
                    maxLines++;

            // Find the index of the largest line
            for (int i = 0; i < lines.Count; i++)
                if (lines[i].text.Length == biggest)
                {
                    biggestIndex = i;
                    break;
                }

            // Measure the longest tooltip to get X bounds
            bounds = font.MeasureString(lines[biggestIndex].text);

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
            if (position.X > Engine.screenWidth - bounds.X)
                position.X = Engine.screenWidth - bounds.X;
            if (position.Y > Engine.screenHeight - bounds.Y - ((maxLines - 1) * 20))
                position.Y = Engine.screenHeight - bounds.Y - ((maxLines - 1) * 20);

            // Draw frame
            if (frame)
                Shape.DrawRect((int)position.X - 4, (int)position.Y - 4, (int)bounds.X + 4, (int)(bounds.Y * (lines.Count > 1 ? lines.Count : 1)) + 4, frameColor, frameFillColor);

            // Draw tooltip
            for (int i = 0; i < lines.Count; i++)
                if (lines[i].text != string.Empty)
                {
                    if (lines[i].text != NewLine)
                        Text.DrawText(font, lines[i].text, position, lines[i].color, true);
                    
                    position.Y += 20;
                }
        }
    }
}
