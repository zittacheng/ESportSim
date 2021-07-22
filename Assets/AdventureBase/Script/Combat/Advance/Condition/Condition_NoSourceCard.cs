using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition_NoSourceCard : Condition {

        public override bool Pass(Card Source)
        {
            return !Source.SourceCard || !Source.SourceCard.CombatActive();
        }
    }
}