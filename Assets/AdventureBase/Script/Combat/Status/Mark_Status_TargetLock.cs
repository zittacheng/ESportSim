using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_TargetLock : Mark_Status {

        public override void Stack(Mark_Status M)
        {
            SetKey("Duration", M.GetKey("Duration"));
        }

        public override float PassValue(string Key, float Value)
        {
            if (Key == "TargetLock")
                return 1;
            return base.PassValue(Key, Value);
        }
    }
}