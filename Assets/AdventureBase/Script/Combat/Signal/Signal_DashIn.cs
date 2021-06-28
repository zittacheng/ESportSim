using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Signal_DashIn : Signal {

        public override void EndEffect()
        {
            if (Source.GetSide() == 0)
            {
                if (!CombatControl.Main.FriendlyCards.Contains(Source))
                    return;
                List<Card> temp = new List<Card>();
                temp.Add(Source);
                for (int i = 0; i < CombatControl.Main.FriendlyCards.Count; i++)
                {
                    if (CombatControl.Main.FriendlyCards[i] != Source)
                        temp.Add(CombatControl.Main.FriendlyCards[i]);
                }
                CombatControl.Main.FriendlyCards.Clear();
                for (int i = 0; i < temp.Count; i++)
                    CombatControl.Main.FriendlyCards.Add(temp[i]);
            }
            else if (Source.GetSide() == 1)
            {
                if (!CombatControl.Main.EnemyCards.Contains(Source))
                    return;
                List<Card> temp = new List<Card>();
                temp.Add(Source);
                for (int i = 0; i < CombatControl.Main.EnemyCards.Count; i++)
                {
                    if (CombatControl.Main.EnemyCards[i] != Source)
                        temp.Add(CombatControl.Main.EnemyCards[i]);
                }
                CombatControl.Main.EnemyCards.Clear();
                for (int i = 0; i < temp.Count; i++)
                    CombatControl.Main.EnemyCards.Add(temp[i]);
            }
            base.EndEffect();
        }
    }
}