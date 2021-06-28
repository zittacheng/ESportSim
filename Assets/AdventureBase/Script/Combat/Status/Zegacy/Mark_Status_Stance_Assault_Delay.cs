using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Stance_Assault_Delay : Mark_Status_Mod {

        public override void OutputSignal(Signal S)
        {
            if (Trigger(S) && S.HasKey("Damage"))
            {
                S.SetKey("Damage", (S.GetKey("Damage") + GetKey("DamageIncreaseValue")) * (1 + GetKey("DamageIncrease")));
                Source.RemoveStatus(this);
            }
            base.OutputSignal(S);
        }

        public override void CommonKeys()
        {
            // "DamageIncrease": Damage increase rate
            // "DamageIncreaseValue": Damage increase value
            base.CommonKeys();
        }
    }
}