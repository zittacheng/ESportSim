using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Targeting_Side : Targeting {

        public override Card FindTarget(Card Source)
        {
            List<Card> Cards = new List<Card>();
            for (int i = CombatControl.Main.Cards.Count - 1; i >= 0; i--)
            {
                Card C = CombatControl.Main.Cards[i];
                if (!C || !C.CombatActive())
                    continue;
                if (C.Side == Source.Side)
                    continue;
                Cards.Add(C);
            }
            if (Cards.Count > 0)
                return Cards[Random.Range(0, Cards.Count)];
            return null;
        }

        public override bool CheckTarget(Card Source, Card Target)
        {
            return Source.Side != Target.Side;
        }
    }
}