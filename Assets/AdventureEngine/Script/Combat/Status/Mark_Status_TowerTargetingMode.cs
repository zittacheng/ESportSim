using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_TowerTargetingMode : Mark_Status {

        public override void Stack(Mark_Status M)
        {
            SetKey("Mode", M.GetKey("Mode"));
        }

        public override float PassValue(string Key, float Value)
        {
            if (Key == "TowerTargetingMode")
                return GetKey("Mode");
            return Value;
        }

        public override void CommonKeys()
        {
            // "Mode": Current targeting mode
            base.CommonKeys();
        }
    }
}