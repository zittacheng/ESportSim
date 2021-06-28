using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition_HasStatus : Condition {
        public string StatusKey;

        public override bool Pass(Card Source)
        {
            for (int i = Source.Status.Count - 1; i >= 0; i--)
            {
                if (Source.Status[i] && Source.Status[i].GetID() == StatusKey)
                    return true;
            }
            return false;
        }
    }
}