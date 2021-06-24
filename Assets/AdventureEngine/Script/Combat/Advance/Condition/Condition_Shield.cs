using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition_Shield : Condition {

        public override bool Pass(Card Source)
        {
            return Source.PassValue("Shield", 0) > 0;
        }
    }
}