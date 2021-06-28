using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Speed : Mark_Status {

        public override void Stack(Mark_Status M)
        {
            StackDuration(M);
            base.Stack(M);
        }

        public override float PassValue(string Key, float Value)
        {
            if (Key == "Speed")
                return Value * GetKey("SpeedScale");
            return Value;
        }

        public override void CommonKeys()
        {
            // "SpeedScale": Speed multiplier
            base.CommonKeys();
        }
    }
}