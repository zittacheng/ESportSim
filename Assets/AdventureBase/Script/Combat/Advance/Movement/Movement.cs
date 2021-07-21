using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Movement : Mark {

        public override void TimePassed(float Value)
        {
            base.TimePassed(Value);
        }

        public virtual void Stop()
        {

        }

        public virtual void OnContact()
        {

        }

        public override void CommonKeys()
        {
            // "Speed": Base speed
            // "Priority": Interrupt priority in AddMovements list
            // "TargetPositionX": Target position X
            // "TargetPositionY": Target position Y
            base.CommonKeys();
        }
    }
}