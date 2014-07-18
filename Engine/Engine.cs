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
        public static ContentManager content;
        public static GraphicsDevice device;
        public static GameWindow window;
        public static SpriteBatch batch;

        public static List<Actor> actors = new List<Actor>();
        public static List<Popoff> popoffs = new List<Popoff>();
        public static List<ITween> tweens = new List<ITween>();
        public static List<ParticleSystem> particleSystems = new List<ParticleSystem>();
        public static List<Window> windows = new List<Window>();

        public static int screenWidth;
        public static int screenHeight;

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
                if (popoff.screen)
                    popoff.Draw(batch);

            foreach (ParticleSystem system in particleSystems)
                if (system.screen)
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
                if (!popoff.screen)
                    popoff.Draw(batch);

            foreach (ParticleSystem system in particleSystems)
                if (!system.screen)
                    system.Draw(batch);
        }

        public static void PreUpdate(GameTime dt)
        {
            window.Title = Assembly.GetCallingAssembly().GetName().Name + " (FPS: " + Math.Round(Timing.FPS) + ")";

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
                if (actors[i].color.A == 0)
                    actors.RemoveAt(i);

            // Clean up Popoffs
            for (int i = 0; i < popoffs.Count; i++)
                if (popoffs[i].currentScale <= 0 || popoffs[i].color.A == 0 || popoffs[i].color2.A == 0)
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
