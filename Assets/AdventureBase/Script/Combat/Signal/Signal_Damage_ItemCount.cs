using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Damage_ItemCount : Signal_Damage {

        public override float GetDamageValue(float Base)
        {
            return base.GetDamageValue(Base) * GetFinalMod();
        }

        public float GetFinalMod()
        {
            float a = GetKey("DamageMod");
            if (HasKey("ItemCount") && HasKey("ModChange"))
                a += GetKey("ItemCount") * GetKey("ModChange");
            return a;
        }

        public override void CommonKeys()
        {
            // "DamageMod": Damage mod
            // "ModChange": Damage mod change per stack
            base.CommonKeys();
        }
    }
}