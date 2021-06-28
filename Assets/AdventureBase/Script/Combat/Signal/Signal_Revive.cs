using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Revive : Signal {

        public override void EndEffect()
        {
            if (!Target || Target.CombatActive())
                return;
            Target.Revive();
            Target.Life = Target.GetMaxLife() * GetKey("StartLife");
            base.EndEffect();
        }

        public override void CommonKeys()
        {
            // "StartLife": Remaining life when revived
            base.CommonKeys();
        }
    }
}