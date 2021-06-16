using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Targeting_Life : Targeting {

        public override Card FindTarget(Card Source)
        {
            List<Card> Cards = CombatControl.Main.Cards;
            float Life = -9999;
            List<Card> Targets = new List<Card>();
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
                if (!Cards[i].CombatActive())
                    continue;
                if (Cards[i].GetSide() == Source.GetSide())
                    continue;
                if (Cards[i].GetKey("Untargeted") == 1)
                    continue;
                float a = Cards[i].GetLife();
                if (a > Life)
                {
                    Life = a;
                    Targets.Clear();
                    Targets.Add(Cards[i]);
                }
                else if (a == Life)
                    Targets.Add(Cards[i]);
            }

            if (Targets.Count > 1)
            {
                float Aggro = -9999;
                Card Temp = null;
                for (int i = Cards.Count - 1; i >= 0; i--)
                {
                    if (Cards[i].GetAggro() >= Aggro)
                    {
                        Aggro = Cards[i].GetAggro();
                        Temp = Cards[i];
                    }
                }
                return Temp;
            }
            else if (Targets.Count > 0)
            {
                return Targets[0];
            }
            else
                return null;
        }

        public override bool CheckTarget(Card Source, Card Target)
        {
            return FindTarget(Source) == Target;
        }
    }
}