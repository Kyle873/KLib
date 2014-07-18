using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public class Control
    {
        public Vector2 position = Vector2.Zero;
        public int width = 0;
        public int height = 0;
        public bool inside = false;
        public bool visible = true;

        public Control(Vector2 pos)
        {
            this.position = pos;
        }

        public virtual void Draw(SpriteBatch batch)
        {
            if (!visible)
                return;

            if (Input.mouseX >= position.X &&
                Input.mouseX <= position.X + width &&
                Input.mouseY >= position.Y &&
                Input.mouseY <= position.Y + height)
                inside = true;
            else
                inside = false;
        }

        public virtual void Update(GameTime dt)
        {
            if (!visible)
                return;
        }
    }
}
