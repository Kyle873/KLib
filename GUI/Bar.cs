using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public class Bar : Control
    {
        public float value = 0f;
        public float valueMax = 100f;
        public bool gradient = false;
        public Color color = Color.White;
        public Color fillColor = Color.White;
        public Vector3 fillColorLeft = Vector3.Zero;
        public Vector3 fillColorRight = Vector3.Zero;

        public Bar(Vector2 pos = default(Vector2), int width = 100, int height = 16, float valueMax = 100f, bool gradient = false)
            : base(pos)
        {
            this.width = width;
            this.height = height;
            this.valueMax = valueMax;
            this.gradient = gradient;

            // Default Colors
            this.color = new Color(128, 128, 128);
            this.fillColor = new Color(192, 192, 192);
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);

            int drawWidth = (int)(width / ((width / value) * (valueMax / width)));
            if (drawWidth > width)
                drawWidth = width;

            if (!gradient)
                Shape.DrawRect((int)position.X, (int)position.Y, width, height, color);

            if (value > 0)
            {
                // TODO: Port to MonoGame
                if (gradient)
                {
                    SpriteBatch effectBatch = new SpriteBatch(Engine.device);
                    Texture2D texture = new Texture2D(Engine.device, width, height);
                    Color[] data = new Color[width * height];
                    for (int i = 0; i < width * height; i++)
                        data[i] = Color.White;
                    texture.SetData(data);  
                    Effect effect = Engine.content.Load<Effect>(@"Shaders\BarGradient");
                    effect.Parameters["colorStart"].SetValue(fillColorLeft);
                    effect.Parameters["colorEnd"].SetValue(fillColorRight);
                    effectBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, effect);
                    effectBatch.Draw(texture, position, new Rectangle(0, 0, drawWidth, height), Color.White);
                    effectBatch.End();
                }
                else
                    Shape.DrawRect((int)position.X, (int)position.Y, drawWidth, height, Color.Transparent, fillColor);
            }
        }

        public override void Update(GameTime dt)
        {
            base.Update(dt);
        }
    }
}
