using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Medium_ExplosionZone : Medium_Explosion {

        public override void EffectUpdate(float Value)
        {
            if (GetKey("Delay") > 0)
            {
                ChangeKey("Delay", -Value);
                return;
            }

            ChangeKey("Duration", -Value);
            if (GetKey("Duration") <= 0)
            {
                EndEffect();
                return;
            }
            if (GetKey("CCD") > 0)
            {
                ChangeKey("CCD", -Value);
                return;
            }
            ChangeKey("CCD", GetKey("CoolDown"));
            ExplosionEffect();
        }

        public override void CommonKeys()
        {
            // "Duration": Duration of the effect zone
            // "CoolDown": Cool down between effects
            // "CCD": Current cool down
            base.CommonKeys();
        }
    }
}