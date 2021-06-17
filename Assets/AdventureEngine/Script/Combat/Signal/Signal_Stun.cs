using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Stun : Signal_AddStatus {

        public override void EndEffect()
        {
            if (!Target || Target.PassValue("Stunned") != 0)
                return;
            base.EndEffect();
        }
    }
}