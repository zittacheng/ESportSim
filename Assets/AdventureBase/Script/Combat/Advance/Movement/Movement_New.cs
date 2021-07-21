using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Movement_New : Movement {

        public override void TimePassed(float Value)
        {
            if (!Source.CombatActive())
                return;

            Vector2 New = Source.GetPosition();

            // Movement
            if (Source.GetTarget())
            {
                Source.LookAt(Source.GetTarget().GetPosition());
                float StopRange = Source.PassValue("AutoAttackRange");
                Vector2 T = Source.GetTarget().GetPosition();
                Vector2 Ori = Source.GetPosition();
                if ((T - Ori).magnitude > StopRange)
                {
                    float D = GetSpeed() * Value;
                    if ((T - Ori).magnitude <= D + StopRange)
                        D = (T - Ori).magnitude - StopRange;
                    New = Ori + (T - Ori).normalized * D;
                }
            }

            // Colliding
            Vector2 PushForce = new Vector2();
            bool Pushing = false;
            for (int i = CombatControl.Main.GetCards().Count - 1; i >= 0; i--)
            {
                Card C = CombatControl.Main.GetCards()[i];
                if (!C || !C.CombatActive())
                    continue;
                if (Source.GetKey("Role") == 0 && C.GetKey("Role") != 0 && C.GetSide() != Source.GetSide())
                    continue;
                float a = (C.GetPosition() - Source.GetPosition()).magnitude;
                float b = C.GetKey("Size") + Source.GetKey("Size");
                if (a < b)
                {
                    float v = 1 - (a / b) + 0.3f;
                    //v *= v;
                    PushForce += v * (Source.GetPosition() - C.GetPosition()).normalized;
                    Pushing = true;
                }
            }
            if (Pushing)
                New += PushForce * 4f * Value;

            Source.SetPosition(New);
            base.TimePassed(Value);
        }

        public virtual float GetSpeed()
        {
            if (Source)
                return Source.PassValue("Speed", GetKey("Speed"));
            return GetKey("Speed");
        }
    }
}