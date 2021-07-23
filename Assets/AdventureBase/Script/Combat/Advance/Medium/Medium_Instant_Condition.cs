using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Medium_Instant_Condition : Medium_Instant {
        public List<GameObject> Conditions;

        public override void Effect(Card Target)
        {
            if (!Source || !Source.CardActive())
                return;
            foreach (GameObject G in Conditions)
            {
                Condition C = G.GetComponent<Condition>();
                if (!C.Pass(Source))
                    return;
            }
            base.Effect(Target);
        }
    }
}