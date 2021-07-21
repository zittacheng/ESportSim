using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Targeting_Proxy : Targeting {

        public override Card FindTarget(Card Source)
        {
            List<Card> Cards = new List<Card>();
            for (int i = CombatControl.Main.GetCards().Count - 1; i >= 0; i--)
            {
                if (!CombatControl.Main.GetCards()[i])
                    continue;
                Card C = CombatControl.Main.GetCards()[i];
                if (!C.CombatActive())
                    continue;
                if ((C.GetSide() == Source.GetSide() && GetKey("TargetFriendly") == 0) || (C.GetSide() != Source.GetSide() && GetKey("TargetEnemies") == 0))
                    continue;
                Cards.Add(C);
            }
            float Distance = 9999f;
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
            if (!Source || !Target)
                return false;
            float r = Source.PassValue("AutoAttackRange");
            if ((Source.GetPosition() - Target.GetPosition()).magnitude <= r)
                return true;
            return FindTarget(Source) == Target;
        }

        public override void CommonKeys()
        {
            // "TargetEnemies": Whether the mark should target enemies
            // "TargetFriendly": Whether the mark should target friendly
            base.CommonKeys();
        }
    }
}