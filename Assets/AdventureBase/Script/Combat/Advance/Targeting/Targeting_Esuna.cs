using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Targeting_Esuna : Targeting {

        public override Card FindTarget(Card Source)
        {
            List<Card> Cards = CombatControl.Main.Cards;
            float Aggro = -9999;
            List<Card> Targets = new List<Card>();
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
                if (!Cards[i].CombatActive())
                    continue;
                if (Cards[i].GetSide() != Source.GetSide())
                    continue;
                if (Cards[i].GetKey("Untargeted") == 1)
                    continue;
                bool HasDebuff = false;
                for (int j = Cards[i].Status.Count - 1; j >= 0; j--)
                {
                    if (Cards[i].Status[j] && Cards[i].Status[j].GetKey("Debuff") == 1)
                        HasDebuff = true;
                }
                if (!HasDebuff)
                    continue;
                float a = Cards[i].GetAggro();
                if (a > Aggro)
                {
                    Aggro = a;
                    Targets.Clear();
                    Targets.Add(Cards[i]);
                }
                else if (a == Aggro)
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