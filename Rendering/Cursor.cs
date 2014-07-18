using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public static class Cursor
    {
        public static bool enabled = true;
        public static Texture2D texture = null;
        public static Vector2 position = Vector2.Zero;
        public static Vector2 origin = Vector2.Zero;
        public static Color color = Color.White;
        public static bool spin = false;
        public static float rotation = 0f;
        public static float scale = 1f;
        public static float speed = 1f;

        public static void Init()
        {
            texture = Engine.content.Load<Texture2D>(@"Sprites\Cursor");
            origin = new Vector2(texture.Width / 4, texture.Height / 4);
        }

        public static void Draw(SpriteBatch batch)
        {
            if (!enabled) return;
            
            Sprite.DrawSprite(texture, position, color, rotation, origin, scale);
        }

        public static void Update(GameTime dt)
        {
            if (!enabled) return;

            position.X = Input.mouseX;
            position.Y = Input.mouseY;

            if (spin)
                rotation += Timing.step / (speed * 32f);
        }

        public static Vector2 ToWorldCoords(Vector2 position)
        {
            return Vector2.Transform(position, Camera.transformInvert);
        }
    }
}
