using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Trigger_WarHorn : Mark_Trigger {

        public override void InputSignal(Signal S)
        {
            if (S.GetKey("UseMainSkill") == 1 && Pass(S))
                TryTrigger(Source, 1, new List<string>());
            base.InputSignal(S);
        }
    }
}