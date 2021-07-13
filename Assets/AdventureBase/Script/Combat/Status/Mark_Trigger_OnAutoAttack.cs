using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Trigger_OnAutoAttack : Mark_Trigger {

        public override void InputSignal(Signal S)
        {
            if (S.GetKey("OnAutoAttack") == 1 && Pass(S))
            {
                float Chance = GetChance();
                List<string> AddKeys = new List<string>();
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

        public override void CommonKeys()
        {
            // "Chance": Trigger chance
            // "ChanceMod": Chance change per stack
            // "ItemCountScaling": Whether the proc chance should scale with "ItemCount"
            base.CommonKeys();
        }
    }
}