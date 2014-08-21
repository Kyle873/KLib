using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    [Flags]
    public enum ParticleSystemFlags
    {
        None,
        ColorCurve,
        ScaleCurve,
        RotationCurve,
    }

    public class Particle
    {
        public ParticleSystemFlags Flags { get; set; }
        public ScaleFunc ScaleFunc { get; set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public Color ColorEnd { get; set; }
        public float Rotation { get; set; }
        public float RotationEnd { get; set; }
        public float Scale { get; set; }
        public float ScaleEnd { get; set; }
        public float Time { get; set; }
        public float Lifetime { get; set; }

        private ColorTween colorTween = Tween.AddColor();
        public ColorTween ColorTween
        {
            get { return colorTween; }
            set { colorTween = value; }
        }
        private FloatTween scaleTween = Tween.AddFloat();
        public FloatTween ScaleTween
        {
            get { return scaleTween; }
            set { scaleTween = value; }
        }
        private FloatTween rotationTween = Tween.AddFloat();
        public FloatTween RotationTween
        {
            get { return rotationTween; }
            set { rotationTween = value; }
        }

        public void Start()
        {
            // Color Curve
            if (Flags.HasFlag(ParticleSystemFlags.ColorCurve))
                colorTween.Start(Color, ColorEnd, Lifetime, ScaleFunc);

            // Scale Curve
            if (Flags.HasFlag(ParticleSystemFlags.ScaleCurve))
                scaleTween.Start(Scale, ScaleEnd, Lifetime, ScaleFunc);

            // Rotation Curve
            if (Flags.HasFlag(ParticleSystemFlags.RotationCurve))
                rotationTween.Start(Rotation, RotationEnd, Lifetime, ScaleFunc);
        }
    }

    public class ParticleSystem
    {
        List<Particle> particles = new List<Particle>();

        private ParticleSystemFlags flags = ParticleSystemFlags.ColorCurve;
        public ParticleSystemFlags Flags
        {
            get { return flags; }
            set { flags = value; }
        }
        private ScaleFunc scaleFunc = ScaleFuncs.Linear;
        public ScaleFunc ScaleFunc
        {
            get { return scaleFunc; }
            set { scaleFunc = value; }
        }
        private Vector2 position = Vector2.Zero;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Texture2D sprite = null;
        public Texture2D Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }
        private Vector2 origin = Vector2.Zero;
        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }
        private Color color = Color.White;
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        private Color colorEnd = Color.Transparent;
        public Color ColorEnd
        {
            get { return colorEnd; }
            set { colorEnd = value; }
        }
        private int count = 100;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        private float time = 0f;
        public float Time
        {
            get { return time; }
        }
        private float lifetime = 1f;
        public float Lifetime
        {
            get { return lifetime; }
            set { lifetime = value; }
        }
        private float speed = 1f;
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        private float rotation = 0f;
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        private float rotationEnd = 0f;
        public float RotationEnd
        {
            get { return rotationEnd; }
            set { rotationEnd = value; }
        }
        private float scale = 1f;
        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        private float scaleEnd = 1f;
        public float ScaleEnd
        {
            get { return scaleEnd; }
            set { scaleEnd = value; }
        }
        private byte fade = 1;
        public byte Fade
        {
            get { return fade; }
            set { fade = value; }
        }
        private bool screen = false;
        public bool Screen
        {
            get { return screen; }
            set { screen = value; }
        }
        private bool loop = false;
        public bool Loop
        {
            get { return loop; }
            set { loop = value; }
        }
        private Vector2 direction = Vector2.Zero;
        public Vector2 Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        private Rectangle deviation = Rectangle.Empty;
        public Rectangle Deviation
        {
            get { return deviation; }
            set { deviation = value; }
        }

        public ParticleSystem(Texture2D sprite)
        {
            this.sprite = sprite;
            this.origin = new Vector2(sprite.Width / 2, sprite.Height / 2);

            Engine.ParticleSystems.Add(this);
        }

        public ParticleSystem(string path)
        {
            this.sprite = Engine.Content.Load<Texture2D>(path);
            this.origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            
            Engine.ParticleSystems.Add(this);
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (Particle particle in particles)
                KLib.Sprite.DrawSprite(sprite, particle.Position, particle.Color, (int)(sprite.Width * particle.Scale), (int)(sprite.Height * particle.Scale), 0, 0, 0, 0, particle.Rotation, origin, particle.Scale);
        }

        public void Update(GameTime dt)
        {
            // Increment timers
            time += Timing.Step / 60f;
            foreach (Particle particle in particles)
                particle.Time += Timing.Step / 60f;

            // Create new particle
            if ((time <= lifetime || loop) && particles.Count < count)
            {
                // Deviation
                float x = position.X + (int)Utils.Random.Next(deviation.X, deviation.Width);
                float y = position.Y + (int)Utils.Random.Next(deviation.Y, deviation.Height);

                Particle particle = new Particle();
                particle.Flags = flags;
                particle.ScaleFunc = scaleFunc;
                particle.Position = new Vector2(x, y);
                particle.Color = color;
                particle.ColorEnd = colorEnd;
                particle.Scale = scale;
                particle.ScaleEnd = scaleEnd;
                particle.Rotation = rotation;
                particle.RotationEnd = rotationEnd;
                particle.Time = 0;
                particle.Lifetime = lifetime;
                particle.Start();
                particles.Add(particle);
            }

            // Update Particles
            foreach (Particle particle in particles)
            {
                // Position
                particle.Position += direction * speed;

                // Curves
                if (flags.HasFlag(ParticleSystemFlags.ColorCurve))
                    particle.Color = particle.ColorTween.CurrentValue;
                if (flags.HasFlag(ParticleSystemFlags.ScaleCurve))
                    particle.Scale = particle.ScaleTween.CurrentValue;
                if (flags.HasFlag(ParticleSystemFlags.RotationCurve))
                    particle.Rotation = particle.RotationTween.CurrentValue;
            }

            // Destroy Particles
            for (int i = 0; i < particles.Count; i++)
                if (particles[i].Time >= lifetime)
                {
                    particles.RemoveAt(i);
                    break;
                }
        }
    }
}
