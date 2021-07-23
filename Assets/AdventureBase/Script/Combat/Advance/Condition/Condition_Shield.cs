using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition_Shield : Condition {

        public override bool Pass(Card Source)
        {
            if (GetKey("Empty") == 0)
                return Source.PassValue("Shield", 0) > 0;
            else
                return Source.PassValue("Shield", 0) <= 0;
        }

        public override void CommonKeys()
        {
            // "Empty": Whether to pass when the card has no shield
            base.CommonKeys();
        }
    }
}