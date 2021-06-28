using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_DOT : Mark_Status {

        public override void TimePassed(float Value)
        {
            if (Source.CombatActive())
            {
                Source.ChangeLife(-GetKey("Damage") * Value);
            }
            base.TimePassed(Value);
        }

        public override void Stack(Mark_Status M)
        {
            StackDuration(M);
        }

        public override void CommonKeys()
        {
            // Damage: Damage amount per second
            base.CommonKeys();
        }
    }
}