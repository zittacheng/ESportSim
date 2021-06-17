using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class CardGroup : MonoBehaviour {
        public List<Card> Cards;
        public Card CurrentCard;
        public int Side;

        public void Ini()
        {
            foreach (Card C in Cards)
                C.Side = Side;
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

        public void SwitchCard(string Key)
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