using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public static class Text
    {
        static SpriteFont tooltipFont;
        public static SpriteFont TooltipFont
        {
            get { return Text.tooltipFont; }
            set { Text.tooltipFont = value; }
        }

        private static Tooltip tooltip;
        public static Tooltip Tooltip
        {
            get { return Text.tooltip; }
            set { Text.tooltip = value; }
        }

        public static void Init()
        {
        }

        public static void Init(SpriteFont font)
        {
            tooltipFont = font;
        }

        public static void Init(string path)
        {
            tooltipFont = Engine.Content.Load<SpriteFont>(path);
        }

        public static void DrawText(SpriteFont font, string text, Vector2 position, Color color, bool shaded = false, float rotation = 0f, Vector2 origin = default(Vector2), float scale = 1f, SpriteEffects effects = SpriteEffects.None, float depth = 0f)
        {
            if (shaded)
            {
                Engine.Batch.DrawString(font, text, new Vector2(position.X, position.Y - 2), Color.Black, rotation, origin, scale, effects, depth);
                Engine.Batch.DrawString(font, text, new Vector2(position.X, position.Y + 2), Color.Black, rotation, origin, scale, effects, depth);
                Engine.Batch.DrawString(font, text, new Vector2(position.X - 2, position.Y), Color.Black, rotation, origin, scale, effects, depth);
                Engine.Batch.DrawString(font, text, new Vector2(position.X + 2, position.Y), Color.Black, rotation, origin, scale, effects, depth);
            }

            Engine.Batch.DrawString(font, text, position, color, rotation, origin, scale, effects, depth);
        }

        public static void DrawText(string path, string text, Vector2 position, Color color, bool shaded = false, float rotation = 0f, Vector2 origin = default(Vector2), float scale = 1f, SpriteEffects effects = SpriteEffects.None, float depth = 0f)
        {
            SpriteFont font = Engine.Content.Load<SpriteFont>(path);
            
            if (shaded)
            {
                Engine.Batch.DrawString(font, text, new Vector2(position.X, position.Y - 2), Color.Black, rotation, origin, scale, effects, depth);
                Engine.Batch.DrawString(font, text, new Vector2(position.X, position.Y + 2), Color.Black, rotation, origin, scale, effects, depth);
                Engine.Batch.DrawString(font, text, new Vector2(position.X - 2, position.Y), Color.Black, rotation, origin, scale, effects, depth);
                Engine.Batch.DrawString(font, text, new Vector2(position.X + 2, position.Y), Color.Black, rotation, origin, scale, effects, depth);
            }

            Engine.Batch.DrawString(font, text, position, color, rotation, origin, scale, effects, depth);
        }
        
        public static void DrawTooltip(SpriteBatch batch)
        {
            if (tooltip != null)
                tooltip.Draw(batch, tooltipFont);

            tooltip = null;
        }
    }
}
