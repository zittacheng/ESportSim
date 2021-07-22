using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Targeting_ToSourceCard : Targeting {

        public override Card FindTarget(Card Source)
        {
            return Source.SourceCard;
        }
    }
}