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
                List<string> AddKeys = new List<string>();
                AddKeys.Add(KeyBase.Compose("TriggerValue", S.GetKey("Damage")));
                if (HasKey("AddDuration"))
                    AddKeys.Add(KeyBase.Compose("Duration", GetAddDuration()));
                TryTrigger(S.Target, Chance, AddKeys);
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

        public virtual float GetAddDuration()
        {
            float d = 0;
            if (HasKey("AddDuration"))
                d += GetKey("AddDuration");
            if (HasKey("ItemCount") && HasKey("AddDurationMod"))
                d += GetKey("AddDurationMod") * GetKey("ItemCount");
            return d;
        }

        public override void CommonKeys()
        {
            // "Chance": Trigger chance
            // "ChanceMod": Chance change per stack
            // "AddDuration": Inherit duration (if status effect)
            // "AddDurationMod": Inherit duration change per stack
            // "ItemCountScaling": Whether the proc chance should scale with "ItemCount"
            base.CommonKeys();
        }
    }
}