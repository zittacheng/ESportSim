using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Evade : Mark_Status_Mod {

        public override void InputSignal(Signal S)
        {
            if (Trigger(S) && S.HasKey("Damage"))
                S.SetKey("Damage", 0);
            base.InputSignal(S);
        }

        public void StartOfTurn()
        {
            //base.StartOfTurn();
            Source.RemoveStatus(this);
        }
    }
}