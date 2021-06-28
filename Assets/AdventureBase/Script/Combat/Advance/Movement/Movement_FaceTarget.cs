using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Movement_FaceTarget : Movement {

        public override void TimePassed(float Value)
        {
            if (Source.GetTarget())
                Source.LookAt(Source.GetTarget().GetPosition());
            base.TimePassed(Value);
        }
    }
}