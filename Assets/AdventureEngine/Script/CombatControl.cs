using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class CombatControl : MonoBehaviour {
        public static CombatControl Main;
        public float TimeScale = 1;
        public int CurrentTurn;
        public bool InCombat;
        public bool Waiting;
        [Space]
        public float Coin;
        [Space]
        public Card CurrentMC;
        public List<Card> MCs;
        public Card SelectingCard;
        public Card HoldingCard;
        public Mark_Skill SelectingItem;
        public ItemRenderer SelectingItemRenderer;
        public Mark SelectingMark;
        [HideInInspector] public Vector2 SelectingPosition;
        [Space]
        public List<Card> Cores;
        public List<Card> FriendlyCards;
        public List<Card> EnemyCards;
        public List<Card> Cards;
        [HideInInspector] public List<Card> DeathCards;
        public List<Medium> Mediums;
        [Space]
        public float DefaultLevel = 1;

        public void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            // Temp
            EndOfCombatAIProcess(0);
        }

        // Update is called once per frame
        void Update()
        {
            if (Waiting)
                PartyListUpdate();
            AggroUpdate();
        }

        public void PartyListUpdate()
        {
            List<Card> TempI = new List<Card>();
            for (int i = FriendlyCards.Count - 1; i >= 0; i--)
                TempI.Add(FriendlyCards[i]);
            FriendlyCards.Clear();
            while (TempI.Count > 0)
            {
                float a = -99999f;
                Card Temp = null;
                for (int i = TempI.Count - 1; i >= 0; i--)
                {
                    if (TempI[i].GetPosition().y > a)
                    {
                        a = TempI[i].GetPosition().y;
                        Temp = TempI[i];
                    }
                }
                FriendlyCards.Add(Temp);
                TempI.Remove(Temp);
            }
            AggroUpdate();
        }

        public void AggroUpdate()
        {
            // Temp Aggro
            for (int i = 0; i < FriendlyCards.Count; i++)
                FriendlyCards[i].Aggro = i * -0.1f + 10;
            for (int i = 0; i < EnemyCards.Count; i++)
                EnemyCards[i].Aggro = i * -0.1f + 10;
        }

        public void AddItem(GameObject ItemPrefab, float CoinChange)
        {
            Mark_Skill S = ItemPrefab.GetComponent<Mark_Skill>();
            ChangeCoin(CoinChange);
            foreach (Card C in MCs)
                C.AddSkill(S);
        }

        public void RemoveItem(GameObject ItemPrefab, float CoinChange)
        {
            Mark_Skill S = ItemPrefab.GetComponent<Mark_Skill>();
            ChangeCoin(CoinChange);
            foreach (Card C in MCs)
                C.RemoveSkill(S.GetID(), 1);
        }

        public void AddItem(GameObject ItemPrefab, Card Target)
        {
            Mark_Skill S = ItemPrefab.GetComponent<Mark_Skill>();
            Target.AddSkill(S);
        }

        public void RemoveItem(GameObject ItemPrefab, Card Target)
        {
            Mark_Skill S = ItemPrefab.GetComponent<Mark_Skill>();
            Target.RemoveSkill(S.GetID(), 1);
        }

        public void ChangeCoin(float Value)
        {
            Coin += Value;
        }

        public bool CanStartCombat()
        {
            return Waiting;
        }

        public void StartOfCombat()
        {
            Waiting = false;
            CurrentTurn++;

            // UI
            SelectingItem = null;
            SelectingItemRenderer = null;

            // Ini CardList
            Cards.Clear();
            foreach (Card C in FriendlyCards)
                Cards.Add(C);
            foreach (Card C in EnemyCards)
                Cards.Add(C);

            InCombat = true;
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
                if (Cards[i].CombatActive())
                    Cards[i].StartOfCombat();
            }

            // Process
            StartCoroutine("CombatProcessIE");
        }

        public void EndOfCombat()
        {
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
                if (Cards[i].CombatActive())
                    Cards[i].EndOfCombat();
            }
        }

        public void ResetMedium()
        {
            for (int i = Mediums.Count - 1; i >= 0; i--)
                Mediums[i].EndEffect();
        }

        public IEnumerator CombatProcessIE()
        {
            float a = 0;
            int Result;
            while (!VictoryCheck(a, out Result))
            {
                a += CombatTime();
                yield return 0;
            }
            yield return new WaitForSeconds(2f);
            InCombat = false;
            EndOfCombat();
            yield return new WaitForSeconds(2f);
            ResetMedium();
            Revive();
            Waiting = true;

            // Coin
            EndOfCombatCoinChange();
            EndOfCombatAIProcess(Result);
        }

        public void EndOfCombatCoinChange()
        {
            float l = DefaultLevel;
            if (KeyBase.Main)
                l = KeyBase.Main.GetKey("Level");
            float CC = (1 + (1 + 0.1f * l) * CurrentTurn) * 10;
            ChangeCoin(CC);
        }

        public void EndOfCombatAIProcess(int Result)
        {
            for (int i = 0; i < FriendlyCards.Count; i++)
            {
                if (FriendlyCards[i] && FriendlyCards[i].GetComponent<AIControl>())
                {
                    if (Result == 1)
                        FriendlyCards[i].GetComponent<AIControl>().Execute(CurrentTurn, true);
                    else
                        FriendlyCards[i].GetComponent<AIControl>().Execute(CurrentTurn, false);
                }
            }
            for (int i = 0; i < EnemyCards.Count; i++)
            {
                if (EnemyCards[i] && EnemyCards[i].GetComponent<AIControl>())
                {
                    if (Result == -1)
                        EnemyCards[i].GetComponent<AIControl>().Execute(CurrentTurn, true);
                    else
                        EnemyCards[i].GetComponent<AIControl>().Execute(CurrentTurn, false);
                }
            }
        }

        public bool VictoryCheck(float CurrentTime, out int Result)
        {
            bool Victory = true;
            bool Defeat = true;
            for (int i = 0; i < FriendlyCards.Count; i++)
            {
                if (FriendlyCards[i].CombatActive())
                    Defeat = false;
            }
            for (int i = 0; i < EnemyCards.Count; i++)
            {
                if (EnemyCards[i].CombatActive())
                    Victory = false;
            }

            if (Victory && !Defeat)
            {
                Result = 1;
                return true;
            }
            else if (Defeat)
            {
                Result = -1;
                return true;
            }
            else if (CurrentTime >= 240f)
            {
                Result = 0;
                return true;
            }
            Result = 0;
            return false;
        }

        public void Revive()
        {
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
                Cards[i].Revive();
            }
        }

        public List<Card> GetCards()
        {
            return Cards;
        }

        public List<Card> GetCards(int Side, int Active)
        {
            List<Card> Temp = new List<Card>();
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
                if (Active == 1 && !Cards[i].CombatActive())
                    continue;
                if (Active == 0 && Cards[i].CombatActive())
                    continue;
                if (Cards[i].GetSide() != Side)
                    continue;
                Temp.Add(Cards[i]);
            }
            return Temp;
        }

        public void AddCard(Card C)
        {
            Cards.Add(C);
        }

        public void RemoveCard(Card C)
        {
            if (Cards.Contains(C))
                Cards.Remove(C);
        }

        public void OnCardDeath(Card C)
        {
            DeathCards.Add(C);
        }

        public void OnCardRevive(Card C)
        {
            if (DeathCards.Contains(C))
                DeathCards.Remove(C);
        }

        public void AddMedium(Medium M)
        {
            Mediums.Add(M);
        }

        public void RemoveMedium(Medium M)
        {
            if (Mediums.Contains(M))
                Mediums.Remove(M);
        }

        public float CombatTime()
        {
            return Time.deltaTime * TimeScale;
        }
    }
}