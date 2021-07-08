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
            if (this != CombatControl.Main.MCGroup)
                StartCoroutine(SwitchPosition(GetCurrentCard().GetKey("Role")));
        }

        public IEnumerator SwitchPosition(float Role)
        {
            yield return new WaitForSeconds(Random.Range(0.01f, 0.2f));
            //yield return 0;
            if (GetCurrentCard().Side == 1)
            {
                if (Role == 1)
                {
                    Card C = GetCurrentCard();
                    int I = CombatControl.Main.EnemyCards.IndexOf(C);
                    while (I != 0 && I != -1 && CombatControl.Main.EnemyCards[I - 1].GetKey("Role") > 1)
                    {
                        Card T = CombatControl.Main.EnemyCards[I - 1];
                        CombatControl.Main.EnemyCards[I - 1] = C;
                        CombatControl.Main.EnemyCards[I] = T;
                        I = CombatControl.Main.EnemyCards.IndexOf(C);
                    }
                }
                else
                {
                    Card C = GetCurrentCard();
                    int I = CombatControl.Main.EnemyCards.IndexOf(C);
                    while (I != CombatControl.Main.EnemyCards.Count - 1 && I != -1 && CombatControl.Main.EnemyCards[I + 1].GetKey("Role") < Role)
                    {
                        Card T = CombatControl.Main.EnemyCards[I + 1];
                        CombatControl.Main.EnemyCards[I + 1] = C;
                        CombatControl.Main.EnemyCards[I] = T;
                        I = CombatControl.Main.EnemyCards.IndexOf(C);
                    }
                }
            }
            else if (GetCurrentCard().Side == 0)
            {
                if (Role == 1)
                {
                    Card C = GetCurrentCard();
                    int I = CombatControl.Main.FriendlyCards.IndexOf(C);
                    while (I != 0 && I != -1 && CombatControl.Main.FriendlyCards[I - 1].GetKey("Role") > 1)
                    {
                        Card T = CombatControl.Main.FriendlyCards[I - 1];
                        CombatControl.Main.FriendlyCards[I - 1] = C;
                        CombatControl.Main.FriendlyCards[I] = T;
                        I = CombatControl.Main.FriendlyCards.IndexOf(C);
                    }
                }
                else
                {
                    Card C = GetCurrentCard();
                    int I = CombatControl.Main.FriendlyCards.IndexOf(C);
                    while (I != CombatControl.Main.FriendlyCards.Count - 1 && I != -1 && CombatControl.Main.FriendlyCards[I + 1].GetKey("Role") < Role)
                    {
                        Card T = CombatControl.Main.FriendlyCards[I + 1];
                        CombatControl.Main.FriendlyCards[I + 1] = C;
                        CombatControl.Main.FriendlyCards[I] = T;
                        I = CombatControl.Main.FriendlyCards.IndexOf(C);
                    }
                }
            }
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