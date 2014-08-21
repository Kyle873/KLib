using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using FMOD;

namespace KLib
{
    class MusicLayer
    {
        public FMOD.Sound music;
        public FMOD.Channel channel;
        public bool stop = false;
        public FloatTween volumeTween = Tween.AddFloat();
    }

    public static class Sound
    {
        static FMOD.System system;

        static Channel sfxChannel = null;
        static Channel musicChannel = null;
        static List<MusicLayer> musicLayers = new List<MusicLayer>();
        internal static List<MusicLayer> MusicLayers
        {
            get { return Sound.musicLayers; }
        }

        private static float fadeTime = 1f;
        public static float FadeTime
        {
            get { return Sound.fadeTime; }
            set { Sound.fadeTime = value; }
        }

        public static void Init()
        {
            Factory.System_Create(ref system);
            system.init(128, INITFLAGS.NORMAL, IntPtr.Zero);
        }

        public static void PlaySound(string path)
        {
            path = Engine.Content.RootDirectory + "\\" + path;

            FMOD.Sound sound = null;
            system.createSound(path, MODE.HARDWARE | MODE.LOOP_OFF, ref sound);
            system.playSound(CHANNELINDEX.FREE, sound, false, ref sfxChannel);
        }

        public static void AddMusicLayer(string path)
        {
            path = Engine.Content.RootDirectory + "\\" + path;

            MusicLayer instance = new MusicLayer();
            FMOD.Sound layer = null;
            FMOD.Channel channel = new Channel();

            system.createSound(path, MODE.LOOP_NORMAL, ref layer);

            instance.music = layer;
            instance.channel = channel;

            musicLayers.Add(instance);
        }

        public static void RemoveMusicLayer(int layer)
        {
            musicLayers[layer].channel.stop();
            musicLayers[layer].channel = null;
            musicLayers[layer].music = null;
            musicLayers.RemoveAt(layer);
        }

        public static void RemoveAllMusicLayers()
        {
            StopAllMusicLayers(true);

            for (int i = 0; i < musicLayers.Count; i++)
                RemoveMusicLayer(i);
        }

        public static void PlayMusicLayer(int layer)
        {
            bool playing = false;
            musicLayers[layer].channel.isPlaying(ref playing);

            if (playing)
                return;

            if (layer == 0)
                for (int i = 0; i < musicLayers.Count; i++)
                    StopMusicLayer(i, true);

            if (layer > 0)
            {
                uint time = 0;
                musicLayers[0].channel.getPosition(ref time, TIMEUNIT.MS);
                system.playSound(CHANNELINDEX.FREE, musicLayers[layer].music, false, ref musicLayers[layer].channel);
                musicLayers[layer].channel.setPosition(time, TIMEUNIT.MS);
                musicLayers[layer].channel.setVolume(0);
                musicLayers[layer].volumeTween.Start(0f, 1f, fadeTime, ScaleFuncs.Linear);
            }
            else
                system.playSound(CHANNELINDEX.FREE, musicLayers[layer].music, false, ref musicLayers[layer].channel);
        }

        public static void StopMusicLayer(int layer, bool instant = false)
        {
            if (instant)
                musicLayers[layer].channel.stop();
            else
            {
                musicLayers[layer].volumeTween.Start(1f, 0f, fadeTime, ScaleFuncs.Linear);
                musicLayers[layer].stop = true;
            }
        }

        public static void CrossfadeLayer(int sourceLayer, int destLayer)
        {
            if (sourceLayer == destLayer)
                return;

            StopMusicLayer(sourceLayer);
            PlayMusicLayer(destLayer);
        }

        public static void StopAllMusicLayers(bool instant = false)
        {
            for (int i = 0; i < musicLayers.Count; i++)
                StopMusicLayer(i, instant);
        }

        public static void Update(GameTime dt)
        {
            for (int i = 0; i < musicLayers.Count; i++)
            {
                float volume = 0;

                if (musicLayers[i].stop)
                {
                    musicLayers[i].channel.setVolume(musicLayers[i].volumeTween.CurrentValue);
                    musicLayers[i].channel.getVolume(ref volume);

                    if (volume <= 0)
                    {
                        musicLayers[i].channel.stop();
                        musicLayers[i].stop = false;
                    }
                }
                else if (i != 0)
                {
                    musicLayers[i].channel.getVolume(ref volume);

                    if (volume <= 1)
                        musicLayers[i].channel.setVolume(musicLayers[i].volumeTween.CurrentValue);
                }
            }

            system.update();
        }
    }
}
