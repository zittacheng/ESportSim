using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Damage_ShieldScaling : Signal_Damage {

        public override float GetDamageValue(float Base)
        {
            if (Target.PassValue("Shield", 0) > 0)
                return base.GetDamageValue(Base) * GetKey("ShieldScaling");
            return base.GetDamageValue(Base);
        }

        public override void CommonKeys()
        {
            // "ShieldScaling": Damage scaling to shield
            base.CommonKeys();
        }
    }
}