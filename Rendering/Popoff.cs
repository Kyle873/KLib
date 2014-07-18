using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    [Flags]
    public enum PopoffFlag
    {
        None = 0,
        ScaleIn = 1,
        ScaleOut = 2,
        ScalePulse = 4,
        FadeOut = 8,
        SpinIn = 16,
        SpinOut = 32,
    }

    public class Popoff
    {
        private float time = 0f;

        public string text = string.Empty;
        public SpriteFont font = null;
        public Vector2 position = Vector2.Zero;
        public Vector2 origin = Vector2.Zero;
        public Vector2 velocity = Vector2.Zero;
        public PopoffFlag flags = PopoffFlag.None;

        public Color color = Color.White;
        public Color color2 = Color.White;

        public ColorTween colorTween = Tween.AddColor(true);
        public Vector2Tween posTween = Tween.AddVector2(true);
        public FloatTween scaleTween = Tween.AddFloat(true);

        public float rotation = 0f;
        public float scale = 1f;
        public float currentScale = 0f;
        public float lifetime = 1f;
        public float pulseTime = 0.25f;
        public float speed = 8f;

        public float scaleMult = 1f;
        public float fadeMult = 1f;
        public float spinMult = 1f;
        public float spinOver = 1f;

        public float scaleUp = 1f;
        public float scaleDown = 2f;
        public float scaleTime = 0.25f;

        public bool screen = true;

        public Popoff(SpriteFont font, Vector2 position, string text, Color color, float lifetime = 3f, PopoffFlag flags = PopoffFlag.None, bool screen = false)
        {
            this.font = font;
            this.position = position;
            this.origin = new Vector2(font.MeasureString(text).X / 2, font.MeasureString(text).Y / 2);
            this.color = color;
            this.color2 = color;
            this.text = text;
            this.lifetime = lifetime;
            this.time = lifetime;
            this.flags = flags;
            this.scaleDown = scale;
            this.scaleUp = scale * 2f;
            this.screen = screen;
        }

        public Popoff(string path, Vector2 position, string text, Color color, float lifetime = 3f, PopoffFlag flags = PopoffFlag.None, bool screen = false)
        {
            this.font = Engine.content.Load<SpriteFont>(path);
            this.position = position;
            this.origin = new Vector2(font.MeasureString(text).X / 2, font.MeasureString(text).Y / 2);
            this.color = color;
            this.color2 = color;
            this.text = text;
            this.lifetime = lifetime;
            this.time = lifetime;
            this.flags = flags;
            this.scaleDown = scale;
            this.scaleUp = scale * 2f;
            this.screen = screen;
        }

        public void Start()
        {
            colorTween.Start(color, color2, pulseTime, ScaleFuncs.Linear);
            scaleTween.Start(scaleDown, scaleUp, scaleTime, ScaleFuncs.Linear);

            Engine.popoffs.Add(this);
        }

        public void Draw(SpriteBatch batch)
        {
            Text.DrawText(font, text, position, colorTween.CurrentValue, false, MathHelper.ToRadians(rotation), origin, currentScale);
        }

        public void Update(GameTime dt)
        {
            position.X += velocity.X;
            position.Y += velocity.Y;

            if (flags == PopoffFlag.None)
                currentScale = 1f;

            if (flags.HasFlag(PopoffFlag.ScaleIn))
                if (currentScale < scale && time > 0)
                    currentScale += Timing.step * scaleMult / speed;

            if (flags.HasFlag(PopoffFlag.SpinIn))
                if (rotation < 360f * spinOver && time > 0)
                    rotation += Timing.step * spinMult * speed;

            if (flags.HasFlag(PopoffFlag.ScalePulse))
                currentScale = scaleTween.CurrentValue;

            if (currentScale >= scale)
                time -= Timing.step / 60f;

            if (time <= 0)
            {
                if (flags == PopoffFlag.None)
                    currentScale = 0f;

                if (flags.HasFlag(PopoffFlag.ScaleOut))
                    currentScale -= Timing.step * (speed / scaleMult);

                if (flags.HasFlag(PopoffFlag.FadeOut))
                {
                    colorTween.Stop(StopBehavior.AsIs);
                    colorTween.Loop = false;
                    color = colorTween.CurrentValue;
                    color.A -= (byte)(Timing.step * (lifetime * fadeMult * speed));
                    if (color.A <= 16)
                        color.A = 0;
                    colorTween.Start(color, color, 1, ScaleFuncs.Linear);
                }

                if (flags.HasFlag(PopoffFlag.SpinOut))
                    rotation -= Timing.step * spinMult * (speed * 2);
            }
        }
    }
}
