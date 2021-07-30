using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition_Channel : Condition {

        public override bool Pass(Card Source)
        {
            return Source.PassValue("Channel", 0) == GetKey("Positive");
        }

        public override void CommonKeys()
        {
            // "Positive": Whether to pass when the source is Channeling
            base.CommonKeys();
        }
    }
}