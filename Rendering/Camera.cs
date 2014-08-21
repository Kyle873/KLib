using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public static class Camera
    {
        private static Vector2 position = Vector2.Zero;
        public static Vector2 Position
        {
            get { return Camera.position; }
            set { Camera.position = value; }
        }
        private static Matrix transform = Matrix.Identity;
        public static Matrix Transform
        {
            get { return Camera.transform; }
        }
        private static Matrix transformInvert = Matrix.Identity;
        public static Matrix TransformInvert
        {
            get { return Camera.transformInvert; }
        }
        private static Viewport viewport = new Viewport();
        public static Viewport Viewport
        {
            get { return Camera.viewport; }
        }
        private static float rotation = 0f;
        public static float Rotation
        {
            get { return Camera.rotation; }
            set { Camera.rotation = value; }
        }
        private static float zoom = 1f;
        public static float Zoom
        {
            get { return Camera.zoom; }
            set { Camera.zoom = value; }
        }

        public static void Init()
        {
            viewport = Engine.Device.Viewport;
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
