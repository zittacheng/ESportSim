using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Condition_HasStatus : Condition {
        public string StatusKey;
        public string RequiredKey;

        public override bool Pass(Card Source)
        {
            for (int i = Source.Status.Count - 1; i >= 0; i--)
            {
                if (!Source.Status[i])
                    continue;
                Mark_Status MS = Source.Status[i];
                if (StatusKey != "" && MS.GetID() == StatusKey)
                    return true;
                if (RequiredKey != "" && MS.GetKey(RequiredKey) > 0)
                    return true;
            }
            return false;
        }
    }
}