using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Heal_MaxLife_ItemCount : Signal_Heal_MaxLife {

        public override float GetHealValue(float Base)
        {
            return base.GetHealValue(Base) * GetFinalMod();
        }

        public float GetFinalMod()
        {
            float a = GetKey("HealMod");
            if (HasKey("ItemCount") && HasKey("ModChange"))
                a += GetKey("ItemCount") * GetKey("ModChange");
            return a;
        }

        public override void CommonKeys()
        {
            // "HealMod": Heal value mod
            // "ModChange": Heal value mod change per stack
            base.CommonKeys();
        }
    }
}