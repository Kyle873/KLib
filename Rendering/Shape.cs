using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public static class Shape
    {
        private static Texture2D pixel;
        public static Texture2D Pixel
        {
            get { return Shape.pixel; }
            set { Shape.pixel = value; }
        }

        public static void Init()
        {
            // Init pixel texture for drawing shapes
            pixel = new Texture2D(Engine.Device, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
        }

        public static void DrawLine(int x, int y, int width, int height, Color color, float rotation = 0f)
        {
            Engine.Batch.Draw(pixel, new Rectangle(x, y, width, height), null, color, rotation, Vector2.Zero, SpriteEffects.None, 0f);
        }

        public static void DrawRect(int x, int y, int width, int height, Color outlineColor, Color fillColor = default(Color))
        {
            if (fillColor != default(Color))
                Engine.Batch.Draw(pixel, new Rectangle(x + 1, y + 1, width - 2, height - 2), fillColor);

            Engine.Batch.Draw(pixel, new Rectangle(x, y, width - 1, 1), outlineColor);
            Engine.Batch.Draw(pixel, new Rectangle(x + width - 1, y, 1, height - 1), outlineColor);
            Engine.Batch.Draw(pixel, new Rectangle(x + 1, y + height - 1, width - 1, 1), outlineColor);
            Engine.Batch.Draw(pixel, new Rectangle(x, y + 1, 1, height - 1), outlineColor);
        }

        public static void DrawCircle(int x, int y, int radius, Color color, bool fill = false)
        {
            for (int i = radius; i >= 0; i--)
            {
                for (double j = 0.0; j < 360.0; j += 1.0)
                {
                    double xPos;
                    double yPos;

                    if (fill)
                    {
                        xPos = x + (radius - i) * Math.Cos(j * Math.PI / 180.0);
                        yPos = y + (radius - i) * Math.Sin(j * Math.PI / 180.0);
                    }
                    else
                    {
                        xPos = (int)Math.Round(x + (radius) * Math.Cos(j * Math.PI / 180.0));
                        yPos = (int)Math.Round(y + (radius) * Math.Sin(j * Math.PI / 180.0));
                    }

                    Engine.Batch.Draw(pixel, new Vector2((float)xPos, (float)yPos), color);
                }

                if (!fill)
                    break;
            }
        }
    }
}
