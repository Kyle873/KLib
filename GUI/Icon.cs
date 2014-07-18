using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public class Icon : Control
    {
        public Texture2D icon = null;
        public Color color = Color.White;
        public Rectangle size = new Rectangle();
        public float scale = 1f;
        public bool insideBorder = false;

        public Icon(Vector2 pos, Texture2D icon, int width = 16, int height = 16)
            : base(pos)
        {
            this.position = pos;
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
