using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public class Label : Control
    {
        private string text = string.Empty;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        private Color color = Color.White;
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        private SpriteFont font = null;
        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }
        private float scale = 1f;
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        private bool shaded = false;
        public bool Shaded
        {
            get { return shaded; }
            set { shaded = value; }
        }

        public Label(SpriteFont font, string text = "", Vector2 position = default(Vector2))
            : base(position)
        {
            this.text = text;
            this.font = font;
            this.position = position;
        }

        public override void Draw(SpriteBatch batch)
        {
            if (!visible)
                return;

            base.Draw(batch);

            KLib.Text.DrawText(font, text, position, color, shaded, 0f, Vector2.Zero, scale);
        }

        public override void Update(GameTime dt)
        {
            if (!visible)
                return;

            base.Update(dt);
        }
    }
}
