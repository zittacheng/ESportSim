using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Medium_Explosion : Medium {
        public List<GameObject> SubSignals;

        public override void EffectUpdate(float Value)
        {
            ChangeKey("Delay", -Value);
            if (GetKey("Delay") <= 0)
            {
                ExplosionEffect();
                EndEffect();
            }
        }

        public void ExplosionEffect()
        {
            if (!HasKey("Range"))
                SetKey("Range", 99999);
            List<Card> Cards = new List<Card>();
            List<Card> TempList = new List<Card>();
            if (GetKey("TargetEnemies") > 0)
            {
                TempList = Targeting.EnemiesInRange(new Vector2(GetKey("PositionX"), GetKey("PositionY")), GetKey("Range"), null, Source.GetSide());
                foreach (Card C in TempList)
                    Cards.Add(C);
            }
            else if (GetKey("TargetFriendly") > 0)
            {
                Cards = Targeting.FriendlyInRange(new Vector2(GetKey("PositionX"), GetKey("PositionY")), GetKey("Range"), null, Source.GetSide());
                foreach (Card C in TempList)
                    Cards.Add(C);
            }
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
                if (GetKey("IgnoreTarget") > 1 && Cards[i] == Target)
                    continue;
                if (GetKey("IgnoreSource") > 1 && Cards[i] == Source)
                    continue;
                if (Cards[i] == Target)
                    Effect(Cards[i]);
                else
                    SubEffect(Cards[i]);
            }
        }

        public virtual void SubEffect(Card Target)
        {
            if (!Source || !Source.CardActive())
                return;
            List<string> AddKeys = new List<string>();
            AddKeys.Add(KeyBase.Compose("TargetPositionX", GetKey("PositionX")));
            AddKeys.Add(KeyBase.Compose("TargetPositionY", GetKey("PositionY")));
            foreach (GameObject G in SubSignals)
                Source.SendSignal(G, AddKeys, Target);
        }

        public override void CommonKeys()
        {
            // "TargetEnemies": Whether the medium should target enemies
            // "TargetFriendly": Whether the medium should target friendly
            // "Delay": The delay before final effect
            // "Range": Range for targeting
            // "IgnoreTarget": Whether the explosion should exclude the original target
            // "IgnoreSource": Whether the explosion should exclude the source
            base.CommonKeys();
        }
    }
}