using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Charge : Signal_AddMovement {

        public override void EndEffect()
        {
            Vector2 Ori = Source.GetPosition();
            Vector2 T = Target.GetPosition();
            Vector2 End;
            if ((Ori - T).magnitude <= GetKey("StopRange"))
                End = Ori;
            else
                End = T - (T - Ori).normalized * GetKey("StopRange");
            SetKey("TargetPositionX", End.x);
            SetKey("TargetPositionY", End.y);
            Target = Source;
            base.EndEffect();
        }

        public override void CommonKeys()
        {
            // "StopRange": Range to stop charging
            base.CommonKeys();
        }
    }
}