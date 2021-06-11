using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Targeting_Proxy : Targeting {

        public override Card FindTarget(Card Source)
        {
            List<Card> Cards = EnemiesInRange(Source.GetPosition(), Source.GetRange(GetKey("Range")), Source, Source.GetSide());
            float Distance = 9999;
            Card Target = null;
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
                float a = (Cards[i].GetPosition() - Source.GetPosition()).magnitude;
                if (a < Distance)
                {
                    Distance = a;
                    Target = Cards[i];
                }
            }
            return Target;
        }

        public override bool CheckTarget(Card Source, Card Target)
        {
            return base.CheckTarget(Source, Target) && GetInRange(Source.GetPosition(), Source.GetRange(GetKey("Range")), Target);
        }
    }
}