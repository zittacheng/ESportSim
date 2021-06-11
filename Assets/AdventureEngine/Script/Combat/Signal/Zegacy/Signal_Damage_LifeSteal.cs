using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Damage_LifeSteal : Signal_Damage {

        public override void SubEndEffect()
        {
            SetKey("Heal", GetKey("Damage") * GetKey("LifeSteal"));
            Source.ChangeLife(GetKey("Heal"));
            base.SubEndEffect();
        }

        public override void CommonKeys()
        {
            // "LifeSteal": Heal rate
            base.CommonKeys();
        }
    }
}