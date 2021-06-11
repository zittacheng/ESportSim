using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_EffectBullet : Signal {
        public GameObject EffectPrefab;
        public Color EffectColor;
        [Space]
        public AnimationCurve Curve;
        public bool OverrideCurve;

        public override void EndEffect()
        {
            if (!Source)
                return;
            if (!HasKey("ParticleScale"))
                SetKey("ParticleScale", 1);
            if (Target && GetKey("IgnoreTarget") <= 0)
                BulletActive(Source.GetPosition(), Target.GetPosition());
            else
                BulletActive(Source.GetPosition(), new Vector2(GetKey("TargetPositionX"), GetKey("TargetPositionY")));
        }

        public void BulletActive(Vector2 StartPoint, Vector2 EndPoint)
        {
            if (!OverrideCurve)
                EffectBullet.NewBullet(StartPoint, EndPoint, EffectPrefab, EffectColor, GetKey("Size"), GetKey("Time"), GetKey("AddDelay"), GetKey("ParticleScale"));
            else
                EffectBullet.NewBullet(StartPoint, EndPoint, EffectPrefab, EffectColor, GetKey("Size"), GetKey("Time"), GetKey("AddDelay"), GetKey("ParticleScale"), Curve);
        }

        public override void CommonKeys()
        {
            // "Size": Size of the effect bullet
            // "Time": Bullet duration
            // "AddDelay": Start delay
            // "ParticleScale": Particle life time scale
            // "IgnoreTarget": Whether the bullet should be position based
            base.CommonKeys();
        }
    }
}