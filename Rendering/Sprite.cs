using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public static class Sprite
    {
        public static void Init()
        {
        }

        public static void DrawSprite(Texture2D texture, Vector2 position, Color color, float rotation = 0f, Vector2 origin = default(Vector2), float scale = 1f, SpriteEffects effects = SpriteEffects.None, float depth = 0f)
        {
            Engine.Batch.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height), color, rotation, origin, scale, effects, depth);
        }

        public static void DrawSprite(string path, Vector2 position, Color color, float rotation = 0f, Vector2 origin = default(Vector2), float scale = 1f, SpriteEffects effects = SpriteEffects.None, float depth = 0f)
        {
            Texture2D texture = Engine.Content.Load<Texture2D>(path);
            Engine.Batch.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height), color, rotation, origin, scale, effects, depth);
        }

        public static void DrawSprite(Texture2D texture, Vector2 position, Color color, int width, int height, int texX = 0, int texY = 0, int texWidth = 0, int texHeight = 0, float rotation = 0f, Vector2 origin = default(Vector2), float scale = 1f, SpriteEffects effects = SpriteEffects.None, float depth = 0f)
        {
            if (texWidth == 0)
                texWidth = texture.Width;
            if (texHeight == 0)
                texHeight = texture.Height;

            Engine.Batch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, width, height), new Rectangle(texX, texY, texWidth, texHeight), color, rotation, origin, effects, depth);
        }

        public static void DrawSprite(string path, Vector2 position, Color color, int width, int height, int texX = 0, int texY = 0, int texWidth = 0, int texHeight = 0, float rotation = 0f, Vector2 origin = default(Vector2), float scale = 1f, SpriteEffects effects = SpriteEffects.None, float depth = 0f)
        {
            Texture2D texture = Engine.Content.Load<Texture2D>(path);

            if (texWidth == 0)
                texWidth = texture.Width;
            if (texHeight == 0)
                texHeight = texture.Height;

            Engine.Batch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, width, height), new Rectangle(texX, texY, texWidth, texHeight), color, rotation, origin, effects, depth);
        }
    }
}
