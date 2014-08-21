using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public static class Cursor
    {
        private static bool enabled = true;
        public static bool Enabled
        {
            get { return Cursor.enabled; }
            set { Cursor.enabled = value; }
        }
        private static Texture2D texture = null;
        public static Texture2D Texture
        {
            get { return Cursor.texture; }
            set { Cursor.texture = value; }
        }
        private static Vector2 position = Vector2.Zero;
        public static Vector2 Position
        {
            get { return Cursor.position; }
            set { Cursor.position = value; }
        }
        private static Vector2 origin = Vector2.Zero;
        public static Vector2 Origin
        {
            get { return Cursor.origin; }
        }
        private static Color color = Color.White;
        public static Color Color
        {
            get { return Cursor.color; }
            set { Cursor.color = value; }
        }
        private static bool spin = false;
        public static bool Spin
        {
            get { return Cursor.spin; }
            set { Cursor.spin = value; }
        }
        private static float rotation = 0f;
        public static float Rotation
        {
            get { return Cursor.rotation; }
        }
        private static float scale = 1f;
        public static float Scale
        {
            get { return Cursor.scale; }
            set { Cursor.scale = value; }
        }
        private static float spinSpeed = 1f;
        public static float SpinSpeed
        {
            get { return Cursor.spinSpeed; }
            set { Cursor.spinSpeed = value; }
        }

        public static void Init()
        {
            texture = Engine.Content.Load<Texture2D>(@"Sprites\Cursor");
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

            position.X = Input.MouseX;
            position.Y = Input.MouseY;

            if (spin)
                rotation += Timing.Step / (spinSpeed * 32f);
        }

        public static Vector2 ToWorldCoords(Vector2 position)
        {
            return Vector2.Transform(position, Camera.TransformInvert);
        }
    }
}
