using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_SpeedMod : Mark_Status {

        public override float PassValue(string Key, float Value)
        {
            if (Key == "Speed")
                return Value * GetKey("SpeedMod");
            return base.PassValue(Key, Value);
        }

        public override void CommonKeys()
        {
            // "SpeedMod": Speed mod
            base.CommonKeys();
        }
    }
}