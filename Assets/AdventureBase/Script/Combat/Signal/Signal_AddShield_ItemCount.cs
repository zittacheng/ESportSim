using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_AddShield_ItemCount : Signal_AddShield {

        public override float GetShieldValue()
        {
            return base.GetShieldValue() * GetFinalMod();
        }

        public float GetFinalMod()
        {
            float a = GetKey("ShieldMod");
            if (HasKey("ItemCount") && HasKey("ModChange"))
                a += GetKey("ItemCount") * GetKey("ModChange");
            return a;
        }

        public override void CommonKeys()
        {
            // "ShieldMod": Additional shield value mod
            // "ModChange": Shield value multiplier change per stack
            base.CommonKeys();
        }
    }
}