using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    [Flags]
    public enum ParticleSystemFlag
    {
        None,
        ColorCurve,
        ScaleCurve,
        RotationCurve,
    }

    public class Particle
    {
        public ParticleSystemFlag flags;
        public ScaleFunc scaleFunc;
        public Vector2 position;
        public Color color;
        public Color ColorEnd;
        public float rotation;
        public float rotationEnd;
        public float scale;
        public float scaleEnd;
        public float time;
        public float lifetime;

        public ColorTween colorTween = Tween.AddColor();
        public FloatTween scaleTween = Tween.AddFloat();
        public FloatTween rotationTween = Tween.AddFloat();

        public void Start()
        {
            // Color Curve
            if (flags.HasFlag(ParticleSystemFlag.ColorCurve))
                colorTween.Start(color, ColorEnd, lifetime, scaleFunc);

            // Scale Curve
            if (flags.HasFlag(ParticleSystemFlag.ScaleCurve))
                scaleTween.Start(scale, scaleEnd, lifetime, scaleFunc);

            // Rotation Curve
            if (flags.HasFlag(ParticleSystemFlag.RotationCurve))
                rotationTween.Start(rotation, rotationEnd, lifetime, scaleFunc);
        }
    }

    public class ParticleSystem
    {
        List<Particle> particles = new List<Particle>();

        public ParticleSystemFlag flags = ParticleSystemFlag.ColorCurve;
        public ScaleFunc scaleFunc = ScaleFuncs.Linear;
        public Vector2 position = Vector2.Zero;
        public Texture2D sprite = null;
        public Vector2 origin = Vector2.Zero;
        public Color color = Color.White;
        public Color colorEnd = Color.Transparent;
        public int count = 100;
        public float time = 0f;
        public float lifetime = 1f;
        public float speed = 1f;
        public float rotation = 0f;
        public float rotationEnd = 0f;
        public float scale = 1f;
        public float scaleEnd = 1f;
        public byte fade = 1;
        public bool screen = false;
        public bool loop = false;
        public Vector2 direction = Vector2.Zero;
        public Rectangle deviation = Rectangle.Empty;

        public ParticleSystem(Texture2D sprite)
        {
            this.sprite = sprite;
            this.origin = new Vector2(sprite.Width / 2, sprite.Height / 2);

            Engine.particleSystems.Add(this);
        }

        public ParticleSystem(string path)
        {
            this.sprite = Engine.content.Load<Texture2D>(path);
            this.origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            
            Engine.particleSystems.Add(this);
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (Particle particle in particles)
                Sprite.DrawSprite(sprite, particle.position, particle.color, (int)(sprite.Width * particle.scale), (int)(sprite.Height * particle.scale), 0, 0, 0, 0, particle.rotation, origin, particle.scale);
        }

        public void Update(GameTime dt)
        {
            // Increment timers
            time += Timing.step / 60f;
            foreach (Particle particle in particles)
                particle.time += Timing.step / 60f;

            // Create new particle
            if ((time <= lifetime || loop) && particles.Count < count)
            {
                // Deviation
                float x = position.X + (int)Utils.random.Next(deviation.X, deviation.Width);
                float y = position.Y + (int)Utils.random.Next(deviation.Y, deviation.Height);

                Particle particle = new Particle();
                particle.flags = flags;
                particle.scaleFunc = scaleFunc;
                particle.position = new Vector2(x, y);
                particle.color = color;
                particle.ColorEnd = colorEnd;
                particle.scale = scale;
                particle.scaleEnd = scaleEnd;
                particle.rotation = rotation;
                particle.rotationEnd = rotationEnd;
                particle.time = 0;
                particle.lifetime = lifetime;
                particle.Start();
                particles.Add(particle);
            }

            // Update Particles
            foreach (Particle particle in particles)
            {
                // Position
                particle.position += direction * speed;

                // Curves
                if (flags.HasFlag(ParticleSystemFlag.ColorCurve))
                    particle.color = particle.colorTween.CurrentValue;
                if (flags.HasFlag(ParticleSystemFlag.ScaleCurve))
                    particle.scale = particle.scaleTween.CurrentValue;
                if (flags.HasFlag(ParticleSystemFlag.RotationCurve))
                    particle.rotation = particle.rotationTween.CurrentValue;
            }

            // Destroy Particles
            for (int i = 0; i < particles.Count; i++)
                if (particles[i].time >= lifetime)
                {
                    particles.RemoveAt(i);
                    break;
                }
        }
    }
}
