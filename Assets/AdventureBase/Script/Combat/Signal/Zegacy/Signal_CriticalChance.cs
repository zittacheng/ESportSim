using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_CriticalChance : Signal {

        public override void StartEffect()
        {
            SetKey("Crit", Random.Range(0.01f, 0.99f));
            base.StartEffect();
        }

        public override string ReturnKey(out float Value)
        {
            if (GetKey("Crit") <= GetKey("CriticalChance"))
                Value = 1;
            else
                Value = 0;
            return "Critical";
        }

        public override void CommonKeys()
        {
            // "CriticalChance": Ori crit chance
            // "Crit": Actual crit value
            base.CommonKeys();
        }
    }
}