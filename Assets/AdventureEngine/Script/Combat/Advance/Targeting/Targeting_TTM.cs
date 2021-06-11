using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Targeting_TTM : Targeting {

        public override Card FindTarget(Card Source)
        {
            int Mode = (int)Source.PassValue("TowerTargetingMode", GetKey("DefaultMode"));
            List<Card> Cards = EnemiesInRange(Source.GetPosition(), Source.GetRange(GetKey("Range")), Source, Source.Side);
            Card Target = null;
            if (Mode == -1)
            {
                float Distance = 9999;
                for (int i = Cards.Count - 1; i >= 0; i--)
                {
                    float a = (Cards[i].GetPosition() - Source.GetPosition()).magnitude;
                    if (a < Distance)
                    {
                        Distance = a;
                        Target = Cards[i];
                    }
                }
            }
            /*else if (Mode == 0 || Mode == 1)
            {
                float Distance = Mathf.Infinity;
                for (int i = Cards.Count - 1; i >= 0; i--)
                {
                    PathFinder_Legacy PF = Cards[i].GetPathFinder();
                    if (!PF)
                        continue;
                    float a = PF.GetExitDistance(Cards[i].GetPosition());
                    if (a < Distance)
                    {
                        Distance = a;
                        Target = Cards[i];
                    }
                }
            }
            else if (Mode == 2)
            {
                float Distance = -1f;
                for (int i = Cards.Count - 1; i >= 0; i--)
                {
                    PathFinder_Legacy PF = Cards[i].GetPathFinder();
                    if (!PF)
                        continue;
                    float a = PF.GetExitDistance(Cards[i].GetPosition());
                    if (a > Distance)
                    {
                        Distance = a;
                        Target = Cards[i];
                    }
                }
            }
            else if (Mode == 3)
            {
                float Life = Mathf.Infinity;
                List<Card> Cs = new List<Card>();
                for (int i = Cards.Count - 1; i >= 0; i--)
                {
                    float a = Cards[i].GetLife();
                    if (a < Life)
                    {
                        Life = a;
                        Cs = new List<Card>();
                        Cs.Add(Cards[i]);
                    }
                    else if (a == Life)
                        Cs.Add(Cards[i]);
                }
                if (Cs.Count > 0)
                    Target = Cs[Random.Range(0, Cs.Count)];
            }
            else if (Mode == 4)
            {
                float Life = -1;
                List<Card> Cs = new List<Card>();
                for (int i = Cards.Count - 1; i >= 0; i--)
                {
                    float a = Cards[i].GetLife();
                    if (a > Life)
                    {
                        Life = a;
                        Cs = new List<Card>();
                        Cs.Add(Cards[i]);
                    }
                    else if (a == Life)
                        Cs.Add(Cards[i]);
                }
                if (Cs.Count > 0)
                    Target = Cs[Random.Range(0, Cs.Count)];
            }*/
            if (!Target)
                Target = FindTarget_Default(Source, Cards);
            return Target;
        }

        public Card FindTarget_Default(Card Source, List<Card> Cards)
        {
            Card Target = null;
            float Distance = 9999;
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

        public override void CommonKeys()
        {
            // "DefaultMod": Default targeting mode
            // "TowerTargetingMode": In which way should the source find its target?
            // [0]: Nearest to source
            // [1]: Nearest to exit
            // [2]: Farthest to exit
            // [3]: Least life
            // [4]: Most life
            base.CommonKeys();
        }
    }
}