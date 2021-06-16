using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_ManaDrain : Mark_Status {

        public override void ReturnSignal(Signal S)
        {
            if (!S.Target || !S.Target.CombatActive())
                return;
            if (S.HasKey("Damage") && S.GetKey("Damage") > 0 && Pass(S))
            {
                float r = S.GetKey("Damage") / S.Target.GetMaxLife();
                float m = r * GetKey("DrainMod");
                S.Target.PassValue("ManaChange", -m);
            }
            base.ReturnSignal(S);
        }

        public virtual float GetChance()
        {
            float a = 1;
            return a;
        }

        public virtual bool Pass(Signal S)
        {
            bool T = true;
            return T;
        }

        public override void CommonKeys()
        {
            // "DrainMod": Drain value scaling
            base.CommonKeys();
        }
    }
}