using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition_Life : Condition {

        public override bool Pass(Card Source)
        {
            float a = Source.GetLife() / Source.GetMaxLife();
            return (a >= GetKey("MinLifeRate") && a <= GetKey("MaxLifeRate")) == (GetKey("Positive") != 0);
        }

        public override void CommonKeys()
        {
            // "Positive": Whether to pass only when value is in range
            // "MaxLifeRate": Max life rate allowed (inclusive)
            // "MinLifeRate": Min life rate allowed (inclusive)
            base.CommonKeys();
        }
    }
}