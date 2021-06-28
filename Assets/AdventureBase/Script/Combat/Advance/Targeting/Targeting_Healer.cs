using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Targeting_Healer : Targeting {

        public override Card FindTarget(Card Source)
        {
            List<Card> Cards = CombatControl.Main.Cards;
            float Life = 1.1f;
            List<Card> Targets = new List<Card>();
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
                if (Cards[i].GetSide() != Source.GetSide())
                    continue;
                if (!Cards[i].CombatActive())
                    continue;
                if (Cards[i].GetLife() >= Cards[i].GetMaxLife())
                    continue;
                if (Cards[i].GetKey("Untargeted") == 1)
                    continue;
                float a = Cards[i].GetLife() / Cards[i].GetMaxLife();
                if (a < Life)
                {
                    Life = a;
                    Targets.Clear();
                    Targets.Add(Cards[i]);
                }
                else if (a == Life)
                    Targets.Add(Cards[i]);
            }
            if (Targets.Count > 0)
                return Targets[Random.Range(0, Targets.Count)];
            else
                return null;
        }

        public override bool CheckTarget(Card Source, Card Target)
        {
            return FindTarget(Source) == Target;
        }
    }
}