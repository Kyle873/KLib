using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public class Label : Control
    {
        public string text = string.Empty;
        public Color color = Color.White;
        public SpriteFont font = null;
        public float scale = 1f;
        public bool fancy = false;

        public Label(SpriteFont font, string text = "", Vector2 pos = default(Vector2))
            : base(pos)
        {
            this.text = text;
            this.font = font;
            this.position = pos;
        }

        public override void Draw(SpriteBatch batch)
        {
            if (!visible)
                return;

            base.Draw(batch);

            Text.DrawText(font, text, position, color, fancy, 0f, Vector2.Zero, scale);
        }

        public override void Update(GameTime dt)
        {
            if (!visible)
                return;

            base.Update(dt);
        }
    }
}
