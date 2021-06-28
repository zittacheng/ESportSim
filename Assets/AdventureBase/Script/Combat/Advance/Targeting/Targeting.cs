using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Targeting : Mark {

        public virtual Card FindTarget(Card Source)
        {
            return null;
        }

        public virtual bool CheckTarget(Card Source, Card Target)
        {
            return Target && Target.CardActive();
        }

        public virtual Vector2 FindPosition(Card Source)
        {
            return new Vector2(Mathf.Infinity, Mathf.Infinity);
        }

        public static List<Card> EnemiesInRange(Vector2 OriPosition, float Range, Card Self, int SelfSide)
        {
            Vector2 Position = OriPosition;
            List<Card> Cards = new List<Card>();
            for (int i = CombatControl.Main.Cards.Count - 1; i >= 0; i--)
            {
                if (GetInRange(Position, Range, CombatControl.Main.Cards[i])
                    && CombatControl.Main.Cards[i] != Self && CombatControl.Main.Cards[i].CombatActive() && CombatControl.Main.Cards[i].GetSide() != SelfSide)
                    Cards.Add(CombatControl.Main.Cards[i]);
            }
            return Cards;
        }

        public static List<Card> FriendlyInRange(Vector2 OriPosition, float Range, Card Self, int SelfSide)
        {
            Vector2 Position = OriPosition;
            List<Card> Cards = new List<Card>();
            for (int i = CombatControl.Main.Cards.Count - 1; i >= 0; i--)
            {
                if (GetInRange(Position, Range, CombatControl.Main.Cards[i])
                    && CombatControl.Main.Cards[i] != Self && CombatControl.Main.Cards[i].CombatActive() && CombatControl.Main.Cards[i].GetSide() == SelfSide)
                    Cards.Add(CombatControl.Main.Cards[i]);
            }
            return Cards;
        }

        public static bool GetInRange(Vector2 OriPosition, float Range, Card Target)
        {
            return (Target.GetPosition() - OriPosition).magnitude <= Range + Target.GetSize();
        }

        public override void CommonKeys()
        {
            // "Range": Range of the targeting mark
            // "Untargeted" (On KeyBase): Whether the card should be ignored by auto targeting
            base.CommonKeys();
        }
    }
}