using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_EffectLine : Signal {
        public GameObject EffectPrefab;
        public Color EffectColor;

        public override void EndEffect()
        {
            if (!Source)
                return;
            if (Target && GetKey("IgnoreTarget") <= 0)
                LineActive(Source.GetPosition(), Target.GetPosition());
            else
                LineActive(Source.GetPosition(), new Vector2(GetKey("TargetPositionX"), GetKey("TargetPositionY")));
        }

        public void LineActive(Vector2 StartPoint, Vector2 EndPoint)
        {
            if (EffectPrefab)
                EffectLine.NewLine(StartPoint, EndPoint, EffectPrefab, EffectColor, GetKey("Width"), GetKey("AddFade"), GetKey("AddDelay"));
            else
                EffectLine.NewLine(StartPoint, EndPoint, EffectColor, GetKey("Width"), GetKey("AddFade"), GetKey("AddDelay"));
        }

        public override void CommonKeys()
        {
            // "Width": Width of the effect line
            // "AddFade": Fade duration change
            // "AddDelay": Start delay
            // "IgnoreTarget": Whether the line should be position based
            base.CommonKeys();
        }
    }
}