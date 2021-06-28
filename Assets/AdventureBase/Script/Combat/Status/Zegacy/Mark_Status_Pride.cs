using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Pride : Mark_Status_DamageOutputMod {

        public override void ReturnSignal(Signal S)
        {
            if (Trigger(S) && S.GetKey("Damage") > 0)
                ChangeKey("Stack", 1); 
            base.ReturnSignal(S);
        }

        public override void StartOfCombat()
        {
            SetKey("Stack", 0);
            base.StartOfCombat();
        }

        public override void EndOfCombat()
        {
            SetKey("Stack", 0);
            base.EndOfCombat();
        }

        public override void CommonKeys()
        {
            // "UnProud": Whether the attack should not trigger Pride
            base.CommonKeys();
        }
    }
}