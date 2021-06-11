using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Mana : Mark_Status {

        public override void TimePassed(float Value)
        {
            if (!Source || !Source.CombatActive())
                return;
            ChangeKey("Mana", Value * Source.PassValue("ManaRecovery", 1));
            if (GetKey("Mana") > GetKey("MaxMana"))
                SetKey("Mana", GetKey("MaxMana"));
            base.TimePassed(Value);
        }

        public override float PassValue(string Key, float Value)
        {
            if (Key == "Mana")
                return GetKey("Mana") / GetKey("MaxMana");
            return base.PassValue(Key, Value);
        }

        public override void InputSignal(Signal S)
        {
            if (S.HasKey("ManaChange"))
                ChangeKey("Mana", S.GetKey("ManaChange") * GetKey("MaxMana"));
            base.InputSignal(S);
        }

        public override void EndOfCombat()
        {
            SetKey("Mana", 0);
            base.EndOfCombat();
        }

        public override void Death()
        {
            SetKey("Mana", 0);
            base.Death();
        }

        public override void CommonKeys()
        {
            // "Mana": Current mana
            // "MaxMana": Maximum mana
            // "ManaRecovery": Mana recovery scale
            base.CommonKeys();
        }
    }
}