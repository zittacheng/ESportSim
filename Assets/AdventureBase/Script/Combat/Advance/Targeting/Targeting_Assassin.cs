using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Targeting_Assassin : Targeting {

        public override Card FindTarget(Card Source)
        {
            List<Card> Cards = CombatControl.Main.Cards;
            float MaxLife = 9999;
            List<Card> Targets = new List<Card>();
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
                if (!Cards[i].CombatActive())
                    continue;
                if (Cards[i].GetSide() == Source.GetSide())
                    continue;
                if (Cards[i].GetKey("Untargeted") == 1)
                    continue;
                float a = Cards[i].GetMaxLife();
                if (a < MaxLife)
                {
                    MaxLife = a;
                    Targets.Clear();
                    Targets.Add(Cards[i]);
                }
                else if (a == MaxLife)
                    Targets.Add(Cards[i]);
            }

            if (Targets.Count > 1)
            {
                float Aggro = -9999;
                Card Temp = null;
                for (int i = Targets.Count - 1; i >= 0; i--)
                {
                    if (Targets[i].GetAggro() >= Aggro)
                    {
                        Aggro = Targets[i].GetAggro();
                        Temp = Targets[i];
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
            return Source.GetSide() != Target.GetSide();
        }
    }
}