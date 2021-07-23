using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_AttackSpeedMod : Mark_Status {

        public override float PassValue(string Key, float Value)
        {
            if (Key == "AttackSpeed" || Key == "RenderingAttackSpeed")
                return Value * GetFinalMod();
            return base.PassValue(Key, Value);
        }

        public float GetFinalMod()
        {
            float a = GetKey("SpeedMod");
            if (HasKey("Stack") && HasKey("StackChange"))
                a += GetKey("Stack") * GetKey("StackChange");
            if (GetKey("ItemCountScaling") != 0 && HasKey("ItemCount") && HasKey("StackChange"))
                a += GetKey("ItemCount") * GetKey("StackChange");
            return a;
        }

        public override void CommonKeys()
        {
            // "SpeedMod": Attack speed multiply rate
            // "StackChange": Attack speed multiplier change per stack
            // "ItemCountScaling": Whether the mod should scale with "ItemCount"
            base.CommonKeys();
        }
    }
}