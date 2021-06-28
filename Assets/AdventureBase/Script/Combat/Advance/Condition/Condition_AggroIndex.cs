using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition_AggroIndex : Condition {

        public override bool Pass(Card Source)
        {
            if (Source.GetSide() == 0)
            {
                int a = CombatControl.Main.FriendlyCards.IndexOf(Source);
                if (a < 0)
                    return false;
                return a >= GetKey("MinIndex") && a <= GetKey("MaxIndex");
            }
            return base.Pass(Source);
        }

        public override void CommonKeys()
        {
            // "MinIndex": Min trigger index (inclusive)
            // "MaxIndex": Max trigger index (inclusive)
            base.CommonKeys();
        }
    }
}