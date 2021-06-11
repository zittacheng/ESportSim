using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_DamageOutputMod : Mark_Status_Mod {

        public override void OutputSignal(Signal S)
        {
            if (Trigger(S) && S.HasKey("Damage"))
            {
                S.SetKey("Damage", S.GetKey("Damage") * GetFinalMod());
                if (HasKey("TriggerCount"))
                {
                    ChangeKey("TriggerCount", -1);
                    if (GetKey("TriggerCount") <= 0)
                        Source.RemoveStatus(this);
                }
            }
            base.OutputSignal(S);
        }

        public float GetFinalMod()
        {
            float a = GetKey("DamageMod");
            if (HasKey("Stack") && HasKey("StackChange"))
                a += GetKey("Stack") * GetKey("StackChange");
            return a;
        }

        public override void CommonKeys()
        {
            // "DamageMod": Damage multiply rate
            // "StackChange": Damage multiplier change per stack
            // "TriggerCount": Remaining trigger count
            base.CommonKeys();
        }
    }
}