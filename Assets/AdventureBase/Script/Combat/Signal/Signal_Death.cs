using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Death : Signal {

        public override void EndEffect()
        {
            if (!Target || (GetKey("CombatActive") > 0 && !Target.CombatActive()))
                return;
            Target.ChangeLife(-Target.GetLife());
            Target.Death();
            base.EndEffect();
        }

        public override void CommonKeys()
        {
            // "CombatActive": Whether to ignore inactive cards
            base.CommonKeys();
        }
    }
}