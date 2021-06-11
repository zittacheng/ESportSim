using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_ColorEffect : Signal {
        public Color color;

        public override void EndEffect()
        {
            if (!Source || !Source.GetAnim())
                return;
            Source.GetAnim().ColorEffect(color);
            base.EndEffect();
        }
    }
}