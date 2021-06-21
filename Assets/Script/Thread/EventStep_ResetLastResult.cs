using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class EventStep_ResetLastResult : EventStep {

        public override void OnEffect()
        {
            KeyBase.Main.SetKey("LastResult", 0);
            base.OnEffect();
        }
    }
}