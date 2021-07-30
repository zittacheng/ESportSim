using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_TauntResist : Mark_Status {

        public override float PassValue(string Key, float Value)
        {
            if (Key == "TauntResist")
                return 1;
            return base.PassValue(Key, Value);
        }
    }
}