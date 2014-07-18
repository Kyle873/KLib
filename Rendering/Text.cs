using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public static class Text
    {
        static SpriteBatch batch;
        static SpriteFont mouseFont;
        
        public static Tooltip tooltip;

        public static void Init()
        {
            batch = Engine.batch;
            mouseFont = Engine.content.Load<SpriteFont>(@"Fonts\TooltipFont");
        }

        public static void DrawText(SpriteFont font, string text, Vector2 position, Color color, bool shaded = false, float rotation = 0f, Vector2 origin = default(Vector2), float scale = 1f, SpriteEffects effects = SpriteEffects.None, float depth = 0f)
        {
            if (shaded)
            {
                batch.DrawString(font, text, new Vector2(position.X, position.Y - 2), Color.Black, rotation, origin, scale, effects, depth);
                batch.DrawString(font, text, new Vector2(position.X, position.Y + 2), Color.Black, rotation, origin, scale, effects, depth);
                batch.DrawString(font, text, new Vector2(position.X - 2, position.Y), Color.Black, rotation, origin, scale, effects, depth);
                batch.DrawString(font, text, new Vector2(position.X + 2, position.Y), Color.Black, rotation, origin, scale, effects, depth);
            }

            batch.DrawString(font, text, position, color, rotation, origin, scale, effects, depth);
        }

        public static void DrawText(string path, string text, Vector2 position, Color color, bool shaded = false, float rotation = 0f, Vector2 origin = default(Vector2), float scale = 1f, SpriteEffects effects = SpriteEffects.None, float depth = 0f)
        {
            SpriteFont font = Engine.content.Load<SpriteFont>(path);
            
            if (shaded)
            {
                batch.DrawString(font, text, new Vector2(position.X, position.Y - 2), Color.Black, rotation, origin, scale, effects, depth);
                batch.DrawString(font, text, new Vector2(position.X, position.Y + 2), Color.Black, rotation, origin, scale, effects, depth);
                batch.DrawString(font, text, new Vector2(position.X - 2, position.Y), Color.Black, rotation, origin, scale, effects, depth);
                batch.DrawString(font, text, new Vector2(position.X + 2, position.Y), Color.Black, rotation, origin, scale, effects, depth);
            }

            batch.DrawString(font, text, position, color, rotation, origin, scale, effects, depth);
        }
        
        public static void DrawTooltip(SpriteBatch batch)
        {
            if (tooltip != null)
                tooltip.Draw(batch, mouseFont);

            tooltip = null;
        }
    }
}
