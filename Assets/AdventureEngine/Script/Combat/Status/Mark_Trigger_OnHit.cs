using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Trigger_OnHit : Mark_Trigger {

        public override void ReturnSignal(Signal S)
        {
            if (!S.Target || !S.Target.CombatActive())
                return;
            if (S.HasKey("Damage") && S.GetKey("Damage") > 0 && Pass(S))
            {
                float Chance = GetChance();
                if (S.HasKey("ProcChance"))
                    Chance *= S.GetKey("ProcChance");
                TryTrigger(S.Target, Chance, new List<string> { KeyBase.Compose("TriggerValue", S.GetKey("Damage")) });
            }
            base.ReturnSignal(S);
        }

        public virtual float GetChance()
        {
            float a = 1;
            if (HasKey("Chance"))
                a = GetKey("Chance");
            if (HasKey("Count") && HasKey("ChanceMod"))
                a += GetKey("ChanceMod") * GetKey("Count");
            if (HasKey("ItemCount") && GetKey("ItemCountScaling") == 1 && HasKey("ChanceMod"))
                a += GetKey("ChanceMod") * GetKey("ItemCount");
            return a;
        }

        public override void CommonKeys()
        {
            // "Chance": Trigger chance
            // "ChanceMod": Chance change per stack
            // "ItemCountScaling": Whether the proc chance should scale with "ItemCount"
            base.CommonKeys();
        }
    }
}