using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_SetTarget : Signal {

        public override void EndEffect()
        {
            if (!Source || !Target || !Target.CombatActive())
                return;
            Source.CurrentTarget = Target;
            base.EndEffect();
        }
    }
}