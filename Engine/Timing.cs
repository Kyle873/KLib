using System;

using Microsoft.Xna.Framework;

namespace KLib
{
    public static class Timing
    {
        private static double delay = 0;

        public static int step = 0;
        public static bool stepped = false;
        public static float timer = 0;
        public static float FPS = 0f;

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

            FPS = 1 / (float)dt.ElapsedGameTime.TotalSeconds;
        }
    }
}
