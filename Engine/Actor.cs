using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public class Actor
    {
        private Texture2D texture = null;
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        private Vector2 position = Vector2.Zero;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private Vector2 velocity = Vector2.Zero;
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        private Rectangle? sourceRect = null;
        public Rectangle? SourceRect
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }

        private Color color = Color.White;
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        private float rotation = 0f;
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        private Vector2 origin = Vector2.Zero;
        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        private Vector2 scale = Vector2.Zero;
        public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        private Effect effect = null;
        public Effect Effect
        {
            get { return effect; }
            set { effect = value; }
        }

        private bool picked = false;
        public bool Picked
        {
            get { return picked; }
            set { picked = value; }
        }

        public Actor()
        {
        }

        public Actor(Texture2D texture, Vector2 position, bool center = true)
        {
            this.texture = texture;
            this.position = position;
            this.scale = new Vector2(texture.Width, texture.Height);

            if (center)
                origin = new Vector2(texture.Width / 2, texture.Height / 2);

            Engine.Actors.Add(this);
        }

        public Actor(string path, Vector2 position, bool center = true)
        {
            this.texture = Engine.Content.Load<Texture2D>(path);
            this.position = position;
            this.scale = new Vector2(texture.Width, texture.Height);

            if (center)
                origin = new Vector2(texture.Width / 2, texture.Height / 2);

            Engine.Actors.Add(this);
        }

        public virtual void Draw(SpriteBatch batch)
        {
            if (effect != null)
            {
                SpriteBatch effectBatch = new SpriteBatch(Engine.Device);
                effectBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, effect, Camera.Transform);
                effectBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, (int)scale.X, (int)scale.Y), sourceRect, color, rotation, origin, SpriteEffects.None, 0f);
                effectBatch.End();
            }
            else
                batch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, (int)scale.X, (int)scale.Y), sourceRect, color, rotation, origin, SpriteEffects.None, 0f);
        }

        public virtual void Update(GameTime dt)
        {
            // Picking
            Vector2 cursorPos = Vector2.Zero;
            if (Cursor.Enabled)
                cursorPos = Cursor.Position;
            else
                cursorPos = new Vector2(Input.MouseX, Input.MouseY);
            Vector2 cursorWorldPos = Cursor.ToWorldCoords(cursorPos);
            if (origin != Vector2.Zero)
            {
                cursorWorldPos.X += (scale.X / 2);
                cursorWorldPos.Y += (scale.Y / 2);
            }
            if (cursorWorldPos.X >= position.X && cursorWorldPos.X <= position.X + scale.X &&
                cursorWorldPos.Y >= position.Y && cursorWorldPos.Y <= position.Y + scale.Y)
                picked = true;
            else
                picked = false;
        }

        public virtual void BasicMove(float speed)
        {
            if (Timing.Stepped)
            {
                position.X += velocity.X;
                velocity.X -= speed;
                position.Y += velocity.Y;
                velocity.Y -= speed;
            }
        }
    }
}
