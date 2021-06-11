using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Damage_x5 : Signal_Damage {

        public override void EndEffect()
        {
            SetKey("Damage", GetKey("Damage") * 5);
            base.EndEffect();
        }
    }
}