using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Heal_LostLife : Signal_Heal {

        public override float GetHealValue(float Base)
        {
            float a = 1;
            if (HasKey("HealScale"))
                a = GetKey("HealScale");
            return (base.GetHealValue(Base) + (Source.GetMaxLife() - Source.GetLife()) * GetKey("LostLifeRate")) * a;
        }

        public override void CommonKeys()
        {
            // "LostLifeRate": Amount of max life add to heal value
            // "HealScale": Add heal scaling
            base.CommonKeys();
        }
    }
}