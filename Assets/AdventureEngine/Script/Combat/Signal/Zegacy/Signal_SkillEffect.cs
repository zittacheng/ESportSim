using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_SkillEffect : Signal {
        public string EffectKey;

        public override void EndEffect()
        {
            if (!Target)
                return;
            //EffectControl.Main.PlayEffect(Target.GetSide(), EffectKey);
            base.EndEffect();
        }
    }
}