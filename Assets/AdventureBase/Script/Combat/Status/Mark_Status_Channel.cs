using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Channel : Mark_Status {

        public override float PassValue(string Key, float Value)
        {
            if (Key == "Stunned")
                return 1;
            if (Key == "Channel")
                return 1;
            if (Key == "Speed")
                return 0;
            return base.PassValue(Key, Value);
        }

        public override void InputSignal(Signal S)
        {
            if (S.GetType() == typeof(Signal_Stun))
            {
                CombatRemove();
                Source.RemoveStatus(this);
            }
            base.InputSignal(S);
        }

        public override void CommonKeys()
        {
            // "Stunned": Whether the source is stunned
            // "Channel": Whether the source is channeling
            base.CommonKeys();
        }
    }
}