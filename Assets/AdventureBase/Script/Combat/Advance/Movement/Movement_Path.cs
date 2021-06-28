using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Movement_Path : Movement {

        public override void TimePassed(float Value)
        {
            /*if (!Source || !Source.GetPathFinder() || Source.GetPathFinder().NextPoint == null)
                return;
            PathFinder_Legacy PF = Source.GetPathFinder();
            Vector2 Ori = Source.GetPosition();
            Vector2 Tar = PF.NextPoint.GetPosition(PF.CurrentStrafe);
            float Distance = GetKey("Speed") * Value;
            if ((Tar - Ori).magnitude <= Distance)
                Source.SetPosition(Tar);
            else
                Source.ChangePosition((Tar - Ori).normalized * Distance);*/
            base.TimePassed(Value);
        }
    }
}