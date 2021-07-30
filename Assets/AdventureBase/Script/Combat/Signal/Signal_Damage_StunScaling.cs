using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Damage_StunScaling : Signal_Damage {

        public override float GetDamageValue(float Base)
        {
            if (Target.PassValue("Stunned", 0) > 0)
                return base.GetDamageValue(Base) * GetKey("StunScaling");
            return base.GetDamageValue(Base);
        }

        public override void CommonKeys()
        {
            // "StunScaling": Damage scaling to Stunned target
            base.CommonKeys();
        }
    }
}