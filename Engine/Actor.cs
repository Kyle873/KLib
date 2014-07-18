using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public class Actor
    {
        public Texture2D texture = null;
        public Vector2 position = Vector2.Zero;
        public Vector2 velocity = Vector2.Zero;
        public Rectangle? sourceRect = null;
        public Color color = Color.White;
        public float rotation = 0f;
        public Vector2 origin = Vector2.Zero;
        public Vector2 scale = Vector2.Zero;
        public Effect effect = null;
        public bool picked = false;

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

            Engine.actors.Add(this);
        }

        public Actor(string path, Vector2 position, bool center = true)
        {
            this.texture = Engine.content.Load<Texture2D>(path);
            this.position = position;
            this.scale = new Vector2(texture.Width, texture.Height);

            if (center)
                origin = new Vector2(texture.Width / 2, texture.Height / 2);

            Engine.actors.Add(this);
        }

        public virtual void Draw(SpriteBatch batch)
        {
            if (effect != null)
            {
                SpriteBatch effectBatch = new SpriteBatch(Engine.device);
                effectBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, effect, Camera.transform);
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
            if (Cursor.enabled)
                cursorPos = Cursor.position;
            else
                cursorPos = new Vector2(Input.mouseX, Input.mouseY);
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
            if (Timing.stepped)
            {
                position.X += velocity.X;
                velocity.X -= speed;
                position.Y += velocity.Y;
                velocity.Y -= speed;
            }
        }
    }
}
