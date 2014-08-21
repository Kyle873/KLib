using System;

using Microsoft.Xna.Framework;

namespace KLib
{
    public static class Timing
    {
        static double delay = 0;

        private static int step = 0;
        public static int Step
        {
            get { return Timing.step; }
            set { Timing.step = value; }
        }
        private static bool stepped = false;
        public static bool Stepped
        {
            get { return Timing.stepped; }
            set { Timing.stepped = value; }
        }
        private static float timer = 0;
        public static float Timer
        {
            get { return Timing.timer; }
            set { Timing.timer = value; }
        }
        private static float fps = 0f;
        public static float Fps
        {
            get { return Timing.fps; }
            set { Timing.fps = value; }
        }

        public static void Update(GameTime dt)
        {
            delay += dt.ElapsedGameTime.TotalSeconds * 60;
            if (delay > 1)
            {
                step = 1;
                stepped = true;
                timer++;
                delay = 0;
            }
            else
            {
                step = 0;
                stepped = false;
            }

            fps = 1 / (float)dt.ElapsedGameTime.TotalSeconds;
        }
    }
}
