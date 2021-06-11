using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Targeting_RandomEnemy : Targeting {

        public override Card FindTarget(Card Source)
        {
            List<Card> Cards = CombatControl.Main.Cards;
            List<Card> Targets = new List<Card>();
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
                if (!Cards[i].CombatActive())
                    continue;
                if (Cards[i].GetSide() == Source.GetSide())
                    continue;
                if (Cards[i].GetKey("Untargeted") == 1)
                    continue;
                Targets.Add(Cards[i]);
            }
            if (Targets.Count > 0)
                return Targets[Random.Range(0, Targets.Count)];
            else
                return null;
        }

        public override bool CheckTarget(Card Source, Card Target)
        {
            return Source.GetSide() != Target.GetSide();
        }
    }
}