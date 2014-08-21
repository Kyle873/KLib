using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public class Icon : Control
    {
        private Texture2D icon = null;
        public Texture2D Icon1
        {
            get { return icon; }
            set { icon = value; }
        }
        private Color color = Color.White;
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        private Rectangle size = new Rectangle();
        public Rectangle Size
        {
            get { return size; }
            set { size = value; }
        }
        private float scale = 1f;
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public Icon(Vector2 position, Texture2D icon, int width = 16, int height = 16)
            : base(position)
        {
            this.position = position;
            this.icon = icon;
            this.width = width;
            this.height = height;
        }

        public override void Draw(SpriteBatch batch)
        {
            if (icon != null)
                batch.Draw(icon, new Rectangle((int)position.X, (int)position.Y, width, height), new Rectangle(0, 0, icon.Width, icon.Height), Color.White);

            base.Draw(batch);
        }

        public override void Update(GameTime dt)
        {
            base.Update(dt);
        }
    }
}
