using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Heal : Signal {

        public override void StartEffect()
        {
            if (HasKey("RHR"))
                SetKey("Heal", Random.Range(GetKey("Heal") - GetKey("RHR"),
                    GetKey("Heal") + GetKey("RHR")));
            if (GetKey("BaseScaling") == 1 && Source)
                SetKey("Heal", GetKey("Heal") * Source.GetBaseDamage());
            SetKey("Heal", GetHealValue(GetKey("Heal")));
            base.StartEffect();
        }

        public override void EndEffect()
        {
            if (GetKey("Heal") < 0)
                SetKey("Heal", 0);
            if (!Target || !Target.CombatActive())
                return;
            Target.ChangeLife(GetKey("Heal"));
            base.EndEffect();
        }

        public virtual float GetHealValue(float Base)
        {
            return Base;
        }

        public override void CommonKeys()
        {
            // "Heal": Final healing value
            // "RHR": Random healing change
            // "BaseScaling": Whether the heal is affected by Base Damage
            base.CommonKeys();
        }
    }
}