using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public class Button : Control
    {
        private Texture2D icon = null;
        public Texture2D Icon
        {
            get { return icon; }
            set { icon = value; }
        }
        private Color color = Color.White;
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        private Color currentColor = Color.White;
        public Color CurrentColor
        {
            get { return currentColor; }
        }
        private SpriteFont font = null;
        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }
        private Vector2 bounds = Vector2.Zero;
        public Vector2 Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }
        private string text = string.Empty;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        private string subText = string.Empty;
        public string SubText
        {
            get { return subText; }
            set { subText = value; }
        }
        private Tooltip tooltip = new Tooltip();
        public Tooltip Tooltip
        {
            get { return tooltip; }
            set { tooltip = value; }
        }
        private bool hoverColorChange = true;
        public bool HoverColorChange
        {
            get { return hoverColorChange; }
            set { hoverColorChange = value; }
        }
        private bool shadedText = false;
        public bool ShadedText
        {
            get { return shadedText; }
            set { shadedText = value; }
        }
        private bool pulse = false;
        public bool Pulse
        {
            get { return pulse; }
            set { pulse = value; }
        }

        public Button(Vector2 position = default(Vector2), int width = 16, int height = 16, string text = "")
            : base(position)
        {
            this.position = position;
            this.width = width;
            this.height = height;
            this.text = text;
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

                if (Input.MouseX >= position.X &&
                    Input.MouseX <= position.X + bounds.X &&
                    Input.MouseY >= position.Y &&
                    Input.MouseY <= position.Y + bounds.Y)
                    inside = true;
                else
                    inside = false;
            }

            if (icon != null)
                batch.Draw(icon, new Rectangle((int)position.X, (int)position.Y, width, height), new Rectangle(0, 0, icon.Width, icon.Height), currentColor);

            if (subText != string.Empty)
                batch.DrawString(font, subText, new Vector2(position.X, position.Y + height - 12), Color.White, 0f, Vector2.Zero, 0.75f, SpriteEffects.None, 0f);

            /* TODO: Port PulseColor into a generic function
            if (pulse)
                currentColor = Utils.PulseColor(color);
            */

            if (inside)
            {
                // TODO: Hover coloring
                if (hoverColorChange)
                {
                    /*
                    if (pulse)
                        currentColor = TerraUtils.PulseColor(Color.Lime);
                    else */
                        currentColor = Color.Lime;
                }

                if (shadedText)
                    KLib.Text.DrawText(font, text, position, currentColor, true);
                else
                    KLib.Text.DrawText(font, text, position, currentColor);

                tooltip.Draw(batch, font);
            }
            else
            {
                if (!pulse)
                    currentColor = color;

                if (shadedText)
                    KLib.Text.DrawText(font, text, position, currentColor, true);
                else
                    KLib.Text.DrawText(font, text, position, currentColor);
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
