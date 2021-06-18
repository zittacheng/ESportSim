using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Trigger_ShieldBreak : Mark_Trigger {

        public override void TimePassed(float Value)
        {
            if (Source.PassValue("Shield", 0) > 0)
                SetKey("HasShield", 1);
            else
            {
                if (GetKey("HasShield") >= 1)
                    TryTrigger(Source, 1, new List<string>());
                SetKey("HasShield", 0);
            }
            base.TimePassed(Value);
        }

        public override void CommonKeys()
        {
            // "HasShield": Whether the souce has shield last frame
            base.CommonKeys();
        }
    }
}