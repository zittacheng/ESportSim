using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Trigger_DeathLock : Mark_Trigger {

        public override void CommonKeys()
        {
            // "Chance": Trigger chance
            // "StackChange": Chance change per stack
            // "ItemCountScaling": Whether the proc chance should scale with "ItemCount"
            base.CommonKeys();
        }
    }
}