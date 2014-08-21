using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public class Bar : Control
    {
        private float value = 0f;
        public float Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        private float valueMax = 100f;
        public float ValueMax
        {
            get { return valueMax; }
            set { valueMax = value; }
        }
        private bool gradient = false;
        public bool Gradient
        {
            get { return gradient; }
            set { gradient = value; }
        }
        private Color color = Color.White;
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        private Color fillColor = Color.White;
        public Color FillColor
        {
            get { return fillColor; }
            set { fillColor = value; }
        }
        private Vector3 fillColorLeft = Vector3.Zero;
        public Vector3 FillColorLeft
        {
            get { return fillColorLeft; }
            set { fillColorLeft = value; }
        }
        private Vector3 fillColorRight = Vector3.Zero;
        public Vector3 FillColorRight
        {
            get { return fillColorRight; }
            set { fillColorRight = value; }
        }

        public Bar(Vector2 position = default(Vector2), int width = 100, int height = 16, float valueMax = 100f, bool gradient = false)
            : base(position)
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
                    SpriteBatch effectBatch = new SpriteBatch(Engine.Device);
                    Texture2D texture = new Texture2D(Engine.Device, width, height);
                    Color[] data = new Color[width * height];
                    for (int i = 0; i < width * height; i++)
                        data[i] = Color.White;
                    texture.SetData(data);  
                    Effect effect = Engine.Content.Load<Effect>(@"Shaders\BarGradient");
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
