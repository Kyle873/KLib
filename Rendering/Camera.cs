using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public static class Camera
    {
        public static Vector2 position = Vector2.Zero;
        public static Matrix transform = Matrix.Identity;
        public static Matrix transformInvert = Matrix.Identity;
        public static Viewport viewport = new Viewport();
        public static float rotation = 0f;
        public static float zoom = 1f;

        public static void Init()
        {
            viewport = Engine.device.Viewport;
        }

        public static void Update(GameTime dt)
        {
            // Clamping
            zoom = MathHelper.Clamp(zoom, 0.1f, 2f);
            rotation = ClampAngle(rotation);

            // Create transform matrix
            transform = Matrix.CreateRotationZ(rotation) * Matrix.CreateScale(new Vector3(zoom, zoom, 1)) * Matrix.CreateTranslation(position.X, position.Y, 0f);
            transformInvert = Matrix.Invert(transform);
        }

        public static float ClampAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
                radians += MathHelper.TwoPi;
            while (radians > MathHelper.Pi)
                radians -= MathHelper.TwoPi;

            return radians;
        }
    }
}
