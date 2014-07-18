using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public class Button : Control
    {
        public Texture2D icon = null;
        public Color color = Color.White;
        public Color currentColor = Color.White;
        public SpriteFont font = null;
        public Vector2 bounds = Vector2.Zero;
        public string text = string.Empty;
        public string subText = string.Empty;
        public List<string> tooltip = new List<string>();
        public List<Color> tooltipColor = new List<Color>();
        public bool hoverColorChange = true;
        public bool shadedText = false;
        public bool pulse = false;

        public Button(Vector2 pos = default(Vector2), int width = 16, int height = 16, string text = "")
            : base(pos)
        {
            this.position = pos;
            this.width = width;
            this.height = height;
            this.text = text;

            /*
            for (int i = 0; i < Text.TooltipSize; i++)
            {
                tooltip.Add(string.Empty);
                tooltipColor.Add(Color.White);
            }
            */
        }

        public override void Draw(SpriteBatch batch)
        {
            if (!visible)
                return;

            base.Draw(batch);

            if (icon == null)
            {
                bounds = font.MeasureString(text);
                bounds.Y -= 8;

                if (Input.mouseX >= position.X &&
                    Input.mouseX <= position.X + bounds.X &&
                    Input.mouseY >= position.Y &&
                    Input.mouseY <= position.Y + bounds.Y)
                    inside = true;
                else
                    inside = false;
            }

            if (icon != null)
                batch.Draw(icon, new Rectangle((int)position.X, (int)position.Y, width, height), new Rectangle(0, 0, icon.Width, icon.Height), currentColor);

            if (subText != string.Empty)
                batch.DrawString(font, subText, new Vector2(position.X, position.Y + height - 12), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);

            /*
            if (pulse)
                currentColor = TerraUtils.PulseColor(color);
            */

            if (inside)
            {
                if (hoverColorChange)
                {
                    /*
                    if (pulse)
                        currentColor = TerraUtils.PulseColor(Color.Lime);
                    else */
                        currentColor = Color.Lime;
                }

                if (shadedText)
                    Text.DrawText(font, text, position, currentColor, true);
                else
                    Text.DrawText(font, text, position, currentColor);

                // TerraHooks.tooltip = tooltip;
                // TerraHooks.tooltipColor = tooltipColor;
            }
            else
            {
                if (!pulse)
                    currentColor = color;

                if (shadedText)
                    Text.DrawText(font, text, position, currentColor, true);
                else
                    Text.DrawText(font, text, position, currentColor);
            }
        }

        public override void Update(GameTime dt)
        {
            if (!visible)
                return;

            base.Update(dt);
        }
    }
}
