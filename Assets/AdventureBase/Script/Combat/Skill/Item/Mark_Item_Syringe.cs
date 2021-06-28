using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Item_Syringe : Mark_Skill {

        public override float PassValue(string Key, float Value)
        {
            if (Key == "AttackSpeed")
                return Value * (1 + GetKey("AttackSpeedMod") * GetKey("Count"));
            return base.PassValue(Key, Value);
        }

        public override void CommonKeys()
        {
            // "AttackSpeedMod": Attack speed mod per stack
            base.CommonKeys();
        }
    }
}