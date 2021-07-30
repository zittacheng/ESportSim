using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Taunt : Signal_AddStatus {

        public override void EndEffect()
        {
            if (Target.PassValue("TauntResisit") == 1)
                return;
            base.EndEffect();
            Target.CurrentTarget = Source;
        }
    }
}