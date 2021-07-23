using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Item_Boots : Mark_Skill {

        public override float PassValue(string Key, float Value)
        {
            if (Key == "Speed")
                return Value * (1 + GetKey("BaseSpeedMod") + GetKey("SpeedMod") * GetKey("Count"));
            return base.PassValue(Key, Value);
        }

        public override void CommonKeys()
        {
            // "BaseSpeedMod": Base movement speed mod
            // "SpeedMod": Movement speed mod per stack
            base.CommonKeys();
        }
    }
}