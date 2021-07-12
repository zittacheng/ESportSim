using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Trigger_Support : Mark_Trigger {

        public override void OutputSignal(Signal S)
        {
            if (S.GetKey("AbilityPowerScaling") == 1 && S.Target && S.Source && S.Source.GetSide() == S.Target.GetSide() && Pass(S))
                TryTrigger(S.Target, 1, new List<string>());
            base.OutputSignal(S);
        }
    }
}