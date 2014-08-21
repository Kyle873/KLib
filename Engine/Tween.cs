using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KLib
{
    public static class Tween
    {
        public static FloatTween AddFloat(bool loop = false)
        {
            FloatTween tween = new FloatTween();
            tween.Loop = loop;
            Engine.Tweens.Add(tween);
            return tween;
        }

        public static ColorTween AddColor(bool loop = false)
        {
            ColorTween tween = new ColorTween();
            tween.Loop = loop;
            Engine.Tweens.Add(tween);
            return tween;
        }

        public static Vector2Tween AddVector2(bool loop = false)
        {
            Vector2Tween tween = new Vector2Tween();
            tween.Loop = loop;
            Engine.Tweens.Add(tween);
            return tween;
        }

        public static Vector3Tween AddVector3(bool loop = false)
        {
            Vector3Tween tween = new Vector3Tween();
            tween.Loop = loop;
            Engine.Tweens.Add(tween);
            return tween;
        }

        public static Vector4Tween AddVector4(bool loop = false)
        {
            Vector4Tween tween = new Vector4Tween();
            tween.Loop = loop;
            Engine.Tweens.Add(tween);
            return tween;
        }

        public static QuaternionTween AddQuaternion(bool loop = false)
        {
            QuaternionTween tween = new QuaternionTween();
            tween.Loop = loop;
            Engine.Tweens.Add(tween);
            return tween;
        }
    }
}
