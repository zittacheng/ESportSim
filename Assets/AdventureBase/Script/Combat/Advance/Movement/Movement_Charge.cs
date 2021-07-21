using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Movement_Charge : Movement {

        public override void TimePassed(float Value)
        {
            if (!HasKey("OriPositionX") || !HasKey("OriPositionY"))
            {
                SetKey("OriPositionX", Source.GetPosition().x);
                SetKey("OriPositionY", Source.GetPosition().y);
            }
            ChangeKey("CurrentTime", Value);
            float S = GetKey("CurrentTime") / GetKey("ChargeTime");
            if (S >= 1)
                S = 0.999f;
            Vector2 Target = new Vector2(GetKey("TargetPositionX"), GetKey("TargetPositionY"));
            Vector2 Ori = new Vector2(GetKey("OriPositionX"), GetKey("OriPositionY"));
            Source.SetPosition(Ori + (Target - Ori) * S);
            if ((Source.GetPosition() - Target).magnitude > 1)
                Source.LookAt(Target);
            if (S >= 0.999f)
                Source.RemoveMovement(this);
            base.TimePassed(Value);
        }

        public override void CommonKeys()
        {
            // "OriPositionX": Original position X (before charge)
            // "OriPositionY": Original position Y (before charge)
            // "ChargeTime": Time to reach target
            // "CurrentTime": Current charge time
            base.CommonKeys();
        }
    }
}