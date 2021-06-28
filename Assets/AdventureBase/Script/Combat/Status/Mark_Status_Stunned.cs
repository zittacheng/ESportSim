using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Stunned : Mark_Status {

        public override float PassValue(string Key, float Value)
        {
            if (Key == "Stunned")
                return 1;
            return base.PassValue(Key, Value);
        }

        public override void CommonKeys()
        {
            // "Stunned": Whether the source is stunned
            base.CommonKeys();
        }
    }
}