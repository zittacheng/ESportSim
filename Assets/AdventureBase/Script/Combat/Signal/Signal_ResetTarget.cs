using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_ResetTarget : Signal {

        public override void EndEffect()
        {
            Source.CurrentTarget = null;
            base.EndEffect();
        }
    }
}