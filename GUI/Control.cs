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

        public Control(Vector2 position)
        {
            this.position = position;
        }

        public virtual void Draw(SpriteBatch batch)
        {
            if (!visible)
                return;

            if (Input.MouseX >= position.X &&
                Input.MouseX <= position.X + width &&
                Input.MouseY >= position.Y &&
                Input.MouseY <= position.Y + height)
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
