using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Movement_RigidbodyControl : Movement {
        public Rigidbody2D Rig;

        public override void TimePassed(float Value)
        {
            if (!Source)
                return;
            Vector2 D = new Vector2(GetKey("InputX"), GetKey("InputY")).normalized * GetSpeed();
            Rig.velocity = D;
            Source.SetPosition(Rig.position);
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