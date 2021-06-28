using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Heal_TriggerValue : Signal_Heal {

        public override float GetHealValue(float Base)
        {
            return base.GetHealValue(Base) * GetKey("TriggerValue") * GetKey("TriggerValueMod");
        }

        public override void CommonKeys()
        {
            // "TriggerValueMod": Mod multiplied by "TriggerValue"
            base.CommonKeys();
        }
    }
}