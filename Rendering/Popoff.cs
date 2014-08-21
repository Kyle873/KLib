using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    [Flags]
    public enum PopoffFlags
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
        float time = 0f;
        private float lifetime = 1f;
        public float Lifetime
        {
            get { return lifetime; }
            set { lifetime = value; }
        }

        private string text = string.Empty;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        private SpriteFont font = null;
        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }
        private Vector2 position = Vector2.Zero;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector2 origin = Vector2.Zero;
        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }
        private Vector2 velocity = Vector2.Zero;
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        private PopoffFlags flags = PopoffFlags.None;
        public PopoffFlags Flags
        {
            get { return flags; }
            set { flags = value; }
        }

        private Color color = Color.White;
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        private Color colorEnd = Color.White;
        public Color ColorEnd
        {
            get { return colorEnd; }
            set { colorEnd = value; }
        }

        ColorTween colorTween = Tween.AddColor(true);
        Vector2Tween posTween = Tween.AddVector2(true);
        FloatTween scaleTween = Tween.AddFloat(true);

        private float rotation = 0f;
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        private float scale = 1f;
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        private float currentScale = 0f;
        public float CurrentScale
        {
            get { return currentScale; }
        }
        
        private float pulseTime = 0.25f;
        public float PulseTime
        {
            get { return pulseTime; }
            set { pulseTime = value; }
        }
        private float pulseSpeed = 8f;
        public float PulseSpeed
        {
            get { return pulseSpeed; }
            set { pulseSpeed = value; }
        }

        private float scaleMult = 1f;
        public float ScaleMult
        {
            get { return scaleMult; }
            set { scaleMult = value; }
        }
        private float fadeMult = 1f;
        public float FadeMult
        {
            get { return fadeMult; }
            set { fadeMult = value; }
        }
        private float spinMult = 1f;
        public float SpinMult
        {
            get { return spinMult; }
            set { spinMult = value; }
        }
        private float spinOver = 1f;
        public float SpinOver
        {
            get { return spinOver; }
            set { spinOver = value; }
        }

        private float scaleUp = 1f;
        public float ScaleUp
        {
            get { return scaleUp; }
            set { scaleUp = value; }
        }
        private float scaleDown = 2f;
        public float ScaleDown
        {
            get { return scaleDown; }
            set { scaleDown = value; }
        }
        private float scaleTime = 0.25f;
        public float ScaleTime
        {
            get { return scaleTime; }
            set { scaleTime = value; }
        }

        private bool screen = true;
        public bool Screen
        {
            get { return screen; }
            set { screen = value; }
        }

        public Popoff(SpriteFont font, Vector2 position, string text, Color color, float lifetime = 3f, PopoffFlags flags = PopoffFlags.None, bool screen = false)
        {
            this.font = font;
            this.position = position;
            this.origin = new Vector2(font.MeasureString(text).X / 2, font.MeasureString(text).Y / 2);
            this.color = color;
            this.colorEnd = color;
            this.text = text;
            this.lifetime = lifetime;
            this.time = lifetime;
            this.flags = flags;
            this.scaleDown = scale;
            this.scaleUp = scale * 2f;
            this.screen = screen;
        }

        public Popoff(string path, Vector2 position, string text, Color color, float lifetime = 3f, PopoffFlags flags = PopoffFlags.None, bool screen = false)
        {
            this.font = Engine.Content.Load<SpriteFont>(path);
            this.position = position;
            this.origin = new Vector2(font.MeasureString(text).X / 2, font.MeasureString(text).Y / 2);
            this.color = color;
            this.colorEnd = color;
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
            colorTween.Start(color, colorEnd, pulseTime, ScaleFuncs.Linear);
            scaleTween.Start(scaleDown, scaleUp, scaleTime, ScaleFuncs.Linear);

            Engine.Popoffs.Add(this);
        }

        public void Draw(SpriteBatch batch)
        {
            KLib.Text.DrawText(font, text, position, colorTween.CurrentValue, false, MathHelper.ToRadians(rotation), origin, currentScale);
        }

        public void Update(GameTime dt)
        {
            position.X += velocity.X;
            position.Y += velocity.Y;

            if (flags == PopoffFlags.None)
                currentScale = 1f;

            if (flags.HasFlag(PopoffFlags.ScaleIn))
                if (currentScale < scale && time > 0)
                    currentScale += Timing.Step * scaleMult / pulseSpeed;

            if (flags.HasFlag(PopoffFlags.SpinIn))
                if (rotation < 360f * spinOver && time > 0)
                    rotation += Timing.Step * spinMult * pulseSpeed;

            if (flags.HasFlag(PopoffFlags.ScalePulse))
                currentScale = scaleTween.CurrentValue;

            if (currentScale >= scale)
                time -= Timing.Step / 60f;

            if (time <= 0)
            {
                if (flags == PopoffFlags.None)
                    currentScale = 0f;

                if (flags.HasFlag(PopoffFlags.ScaleOut))
                    currentScale -= Timing.Step * (pulseSpeed / scaleMult);

                if (flags.HasFlag(PopoffFlags.FadeOut))
                {
                    colorTween.Stop(StopBehavior.AsIs);
                    colorTween.Loop = false;
                    color = colorTween.CurrentValue;
                    color.A -= (byte)(Timing.Step * (lifetime * fadeMult * pulseSpeed));
                    if (color.A <= 16)
                        color.A = 0;
                    colorTween.Start(color, color, 1, ScaleFuncs.Linear);
                }

                if (flags.HasFlag(PopoffFlags.SpinOut))
                    rotation -= Timing.Step * spinMult * (pulseSpeed * 2);
            }
        }
    }
}
