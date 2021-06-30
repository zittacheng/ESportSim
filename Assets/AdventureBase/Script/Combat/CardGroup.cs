using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class CardGroup : MonoBehaviour {
        public List<Card> Cards;
        public Card CurrentCard;
        public string StartKey;
        public int Side;

        public void Ini()
        {
            foreach (Card C in Cards)
                C.Side = Side;
            if (StartKey != "")
                SwitchCard(StartKey);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public Card GetCurrentCard()
        {
            return CurrentCard;
        }

        public AIControl GetAIControl()
        {
            return GetComponent<AIControl>();
        }

        public bool CanSwitch(string Key)
        {
            for (int i = 0; i < CombatControl.Main.FriendlyCards.Count; i++)
            {
                if (CombatControl.Main.FriendlyCards[i].GetInfo().GetID() == Key && CombatControl.Main.FriendlyCards[i].GetSide() == Side)
                    return false;
            }
            for (int i = 0; i < CombatControl.Main.EnemyCards.Count; i++)
            {
                if (CombatControl.Main.EnemyCards[i].GetInfo().GetID() == Key && CombatControl.Main.EnemyCards[i].GetSide() == Side)
                    return false;
            }
            return true;
        }

        public void SwitchCard(string Key)
        {
            print("TrySwitch: " + Key);
            HeroInfo HI = null;
            foreach (HeroInfo Info in GetComponentsInChildren<HeroInfo>())
            {
                if (Info.HeroKey == Key)
                    HI = Info;
            }
            if (!HI)
                return;
            HI.SwitchHero(GetCurrentCard());
        }

        public void SwitchCard_Legacy(string Key)
        {
            Card C = GetCard(Key);
            if (!C || !CurrentCard)
                return;
            if (C == CurrentCard)
                return;
            int Index = -1;
            if (Side == 0)
                Index = CombatControl.Main.FriendlyCards.IndexOf(CurrentCard);
            else if (Side == 1)
                Index = CombatControl.Main.EnemyCards.IndexOf(CurrentCard);
            if (Index == -1)
                return;
            Card Ori = CurrentCard;
            CurrentCard = C;
            if (Side == 0)
                CombatControl.Main.FriendlyCards[Index] = C;
            else if (Side == 1)
                CombatControl.Main.EnemyCards[Index] = C;
            Ori.GetAnim().ForcePosition(C.GetPosition());
            if (Side == 0)
                C.GetAnim().ForcePosition(UIControl.Main.GetFriendlySlotPosition(Index));
            else if (Side == 1)
                C.GetAnim().ForcePosition(UIControl.Main.GetEnemySlotPosition(Index));
        }

        public Card GetCard(string Key)
        {
            foreach (Card C in Cards)
            {
                if (C.GetInfo().GetID() == Key)
                    return C;
            }
            return null;
        }
    }
}