using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Targeting_ToSelf : Targeting {

        public override Card FindTarget(Card Source)
        {
            return Source;
        }

        public override bool CheckTarget(Card Source, Card Target)
        {
            return Target == Source;
        }
    }
}