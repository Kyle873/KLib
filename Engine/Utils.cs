using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public static class Utils
    {
        public static Random random = new Random((int)DateTime.Now.Ticks);

        public static void DumpError(Exception e, string text = "")
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\";
            
            if (text != string.Empty)
            {
                KConsole.Log(text, ConsoleLogLevel.Error);

                if (e != null)
                    KConsole.Log(e.ToString().Split('\n')[0], ConsoleLogLevel.Error);
            }

            if (e != null)
            {
                string[] lines = e.StackTrace.Split('\n');
                foreach (string line in lines)
                    KConsole.Log(line, ConsoleLogLevel.Error);

                string[] dump = { e.ToString(), e.StackTrace };
                File.WriteAllLines(path + "Error-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".log", dump);
            }
        }

        public static void Swap(IList<int> list, int a, int b)
        {
            int tmp = list[a];
            list[a] = list[b];
            list[b] = tmp;
        }
        
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static float Percent(float a, float b)
        {
            return (a * b) / 100f;
        }

        public static float CosineWave(float speed, float radius)
        {
            return (float)(Math.Cos(Timing.timer / speed) * radius);
        }

        public static float SineWave(float speed, float radius)
        {
            return (float)(Math.Sin(Timing.timer / speed) * radius);
        }

        // TODO: MonoGame implementation
        public static void TakeScreenshot(GraphicsDevice device)
        {
            /* XNA 4.0
            int count = 1;
            int maxCoun = 10000;
            int width = device.PresentationParameters.BackBufferWidth;
            int height = device.PresentationParameters.BackBufferHeight;
            int[] data = new int[width * height];
            device.GetBackBufferData(data);

            Texture2D screenshot = new Texture2D(device, width, height, false, device.PresentationParameters.BackBufferFormat);
            screenshot.SetData(data);

            for (int i = 1; i < maxCoun; i++)
            {
                if (File.Exists("Datajack" + i + ".png"))
                    continue;
                else
                {
                    count = i;
                    break;
                }
            }

            Stream stream = File.OpenWrite("Datajack" + count + ".png");
            screenshot.SaveAsPng(stream, width, height);

            screenshot.Dispose();
            stream.Close();
            */
        }
    }
}
