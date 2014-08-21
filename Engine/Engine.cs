using System;
using System.Collections.Generic;
using System.Reflection;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public static class Engine
    {
        private static ContentManager content;
        public static ContentManager Content
        {
            get { return Engine.content; }
        }
        private static GraphicsDevice device;
        public static GraphicsDevice Device
        {
            get { return Engine.device; }
        }
        private static GameWindow window;
        public static GameWindow Window
        {
            get { return Engine.window; }
        }
        private static SpriteBatch batch;
        public static SpriteBatch Batch
        {
            get { return Engine.batch; }
        }

        private static List<Actor> actors = new List<Actor>();
        public static List<Actor> Actors
        {
            get { return Engine.actors; }
        }
        private static List<Popoff> popoffs = new List<Popoff>();
        public static List<Popoff> Popoffs
        {
            get { return Engine.popoffs; }
        }
        private static List<ITween> tweens = new List<ITween>();
        public static List<ITween> Tweens
        {
            get { return Engine.tweens; }
            set { Engine.tweens = value; }
        }
        private static List<ParticleSystem> particleSystems = new List<ParticleSystem>();
        public static List<ParticleSystem> ParticleSystems
        {
            get { return Engine.particleSystems; }
        }
        private static List<Window> windows = new List<Window>();
        public static List<Window> Windows
        {
            get { return Engine.windows; }
        }


        private static int screenWidth;
        public static int ScreenWidth
        {
            get { return Engine.screenWidth; }
        }

        private static int screenHeight;
        public static int ScreenHeight
        {
            get { return Engine.screenHeight; }
        }

        public static void Init(Game game, SpriteBatch spriteBatch)
        {
            content = game.Content;
            device = game.GraphicsDevice;
            window = game.Window;
            batch = spriteBatch;

            KConsole.Init();
            KConsole.Log("KLib: Initializing Console...", ConsoleLogLevel.Internal);

            KConsole.Log("KLib: Initializing Camera...", ConsoleLogLevel.Internal);
            Camera.Init();
            
            KConsole.Log("KLib: Initializing Cursor...", ConsoleLogLevel.Internal);
            Cursor.Init();
            
            KConsole.Log("KLib: Initializing Sound...", ConsoleLogLevel.Internal);
            Sound.Init();
            
            KConsole.Log("KLib: Initializing Sprite...", ConsoleLogLevel.Internal);
            Sprite.Init();
            
            KConsole.Log("KLib: Initializing Shape...", ConsoleLogLevel.Internal);
            Shape.Init();
            
            KConsole.Log("KLib: Initializing Text...", ConsoleLogLevel.Internal);
            Text.Init();
            
            KConsole.Log("KLib: Initialization Done!", ConsoleLogLevel.Internal);
        }

        public static void DrawScreen(SpriteBatch batch)
        {
            foreach (Window window in windows)
                window.Draw(batch);

            foreach (Popoff popoff in popoffs)
                if (popoff.Screen)
                    popoff.Draw(batch);

            foreach (ParticleSystem system in particleSystems)
                if (system.Screen)
                    system.Draw(batch);
            
            Text.DrawTooltip(batch);
            KConsole.Draw(batch);
            Cursor.Draw(batch);
        }

        public static void DrawWorld(SpriteBatch batch)
        {
            foreach (Actor actor in actors)
                actor.Draw(batch);

            foreach (Popoff popoff in popoffs)
                if (!popoff.Screen)
                    popoff.Draw(batch);

            foreach (ParticleSystem system in particleSystems)
                if (!system.Screen)
                    system.Draw(batch);
        }

        public static void PreUpdate(GameTime dt)
        {
            window.Title = Assembly.GetCallingAssembly().GetName().Name + " (FPS: " + Math.Round(Timing.Fps) + ")";

            screenWidth = window.ClientBounds.Width;
            screenHeight = window.ClientBounds.Height;

            Input.PreUpdate(dt);
            Sound.Update(dt);
        }

        public static void PostUpdate(GameTime dt)
        {
            foreach (Actor actor in actors)
                actor.Update(dt);

            foreach (Popoff popoff in popoffs)
                popoff.Update(dt);

            foreach (ITween tween in tweens)
                tween.Update((float)dt.ElapsedGameTime.TotalSeconds);

            foreach (ParticleSystem system in particleSystems)
                system.Update(dt);

            foreach (Window window in windows)
                window.Update(dt);

            // Clean up Actors
            for (int i = 0; i < actors.Count; i++)
                if (actors[i].Color.A == 0)
                    actors.RemoveAt(i);

            // Clean up Popoffs
            for (int i = 0; i < popoffs.Count; i++)
                if (popoffs[i].CurrentScale <= 0 || popoffs[i].Color.A == 0 || popoffs[i].ColorEnd.A == 0)
                    popoffs.RemoveAt(i);

            /* TODO: Clean up Tweens
            for (int i = 0; i < tweens.Count; i++)
                if (tweens[i].State == TweenState.Stopped)
                    tweens.RemoveAt(i); */
            
            // TODO: Clean up Particle Systems

            Input.PostUpdate(dt);
            Camera.Update(dt);
            Cursor.Update(dt);
            KConsole.Update(dt);
            Timing.Update(dt);
        }
    }
}
