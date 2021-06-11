using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Movement_MCC : Movement {

        public override void TimePassed(float Value)
        {
            if (!Source)
                return;
            if (GetKey("InputX") == 0 && GetKey("InputY") == 0)
                return;
            Vector2 D = new Vector2(GetKey("InputX"), GetKey("InputY")).normalized * GetSpeed() * Value;
            Source.ChangePosition(D);
        }

        public virtual float GetSpeed()
        {
            if (Source)
                return Source.PassValue("Speed", GetKey("Speed"));
            return GetKey("Speed");
        }

        public override void CommonKeys()
        {
            // "InputX": Input value x
            // "InputY": Input value y
            base.CommonKeys();
        }
    }
}