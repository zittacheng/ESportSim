using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_SetAnimTrigger : Signal {
        public string TriggerKey;

        public override void StartEffect()
        {
            base.StartEffect();
        }

        public override void EndEffect()
        {
            if (!Target)
                return;
            Target.GetAnim().SetTrigger(TriggerKey);
            base.EndEffect();
        }
    }
}