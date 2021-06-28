using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Movement_AI_FaceCast : Movement {

        public override void TimePassed(float Value)
        {
            if (Source.GetCast())
            {
                if (Source.CastTarget)
                    Source.LookAt(Source.CastTarget.GetPosition());
                else
                    Source.LookAt(Source.CastPosition);
            }
        }
    }
}