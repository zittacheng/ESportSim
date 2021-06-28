using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_Damage : Signal {

        public override void StartEffect()
        {
            if (!Target)
                return;
            if (HasKey("RDR"))
                SetKey("Damage", Random.Range(GetKey("Damage") - GetKey("RDR"),
                    GetKey("Damage") + GetKey("RDR")));
            if (GetKey("CriticalActive") == 1 && GetKey("Critical") >= 1)
                SetKey("Damage", GetKey("Damage") * 2.5f);
            if (GetKey("BaseScaling") == 1 && Source)
                SetKey("Damage", GetKey("Damage") * Source.GetBaseDamage());
            SetKey("Damage", GetDamageValue(GetKey("Damage")));
            base.StartEffect();
        }

        public override void EndEffect()
        {
            if (GetKey("Damage") < 0)
                SetKey("Damage", 0);
            if (!Target || !Target.CombatActive())
                return;
            Target.ChangeLife(-GetKey("Damage"));
            SubEndEffect();
            base.EndEffect();
        }

        public virtual void SubEndEffect()
        {

        }

        public virtual float GetDamageValue(float Base)
        {
            return Base;
        }

        public override void CommonKeys()
        {
            // "Damage": Final damage value
            // "RDR": Random damage change
            // "Physical": Whether the damage is physical
            // "Magical": Whether the damage is magical
            // "CriticalActive": Whether the damage can crit
            // "BaseScaling": Whether the damage is affected by Base Damage

            // "MainHit": Whether the damage is the main trigger damage
            // "SubHit": Whether the damage is the sub trigger damage
            // "AutoAttackHit": Whether the damage is from auto attacks
            // "AbilityHit": Whether the damage is from abilities or items
            // "ProcChance": Proc chance of the damage
            base.CommonKeys();
        }
    }
}