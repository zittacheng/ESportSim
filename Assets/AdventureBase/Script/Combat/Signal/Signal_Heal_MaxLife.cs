using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Heal_MaxLife : Signal_Heal {

        public override float GetHealValue(float Base)
        {
            float a = 1;
            if (HasKey("HealScale"))
                a = GetKey("HealScale");
            return base.GetHealValue(Base) + Source.GetMaxLife() * GetKey("MaxLifeRate") + Source.GetLife() * GetKey("LifeRate") * a;
        }

        public override void CommonKeys()
        {
            // "MaxLifeRate": Amount of max life add to heal value
            // "LifeRate": Amount of life add to heal value
            // "HealScale": Add heal scaling
            base.CommonKeys();
        }
    }
}