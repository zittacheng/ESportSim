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
        public Card HoldingCard;
        public Mark_Skill SelectingItem;
        public ItemRenderer SelectingItemRenderer;
        public Card SelectingCard;
        public SwitchRenderer SelectingSwitch;
        public Mark SelectingMark;
        [HideInInspector] public Vector2 SelectingPosition;
        [Space]
        public CardGroup MCGroup;
        public List<CardGroup> Groups;
        [Space]
        public List<Card> Cores;
        public List<Card> FriendlyCards;
        public List<Card> EnemyCards;
        public List<Card> Cards;
        [HideInInspector] public List<Card> DeathCards;
        public List<Medium> Mediums;
        [Space]
        public float DefaultLevel = 1;
        public bool EndGame;

        public void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {
            DefaultLevel = KeyBase.Main.GetKey("Level");

            GroupIni();
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

        public void GroupIni()
        {
            for (int i = 0; i < Groups.Count; i++)
            {
                Groups[i].Ini();
                if (Groups[i].Side == 0)
                {
                    FriendlyCards.Add(Groups[i].GetCurrentCard());
                    Groups[i].GetCurrentCard().GetAnim().ForcePosition(UIControl.Main.GetFriendlySlotPosition(FriendlyCards.IndexOf(Groups[i].GetCurrentCard())));
                }
                else if (Groups[i].Side == 1)
                {
                    EnemyCards.Add(Groups[i].GetCurrentCard());
                    Groups[i].GetCurrentCard().GetAnim().ForcePosition(UIControl.Main.GetEnemySlotPosition(EnemyCards.IndexOf(Groups[i].GetCurrentCard())));
                }
            }
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
            SelectingSwitch = null;
            SelectingCard = null;

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
                {
                    Cards[i].CurrentTarget = null;
                    Cards[i].StartOfCombat();
                }
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

            if (SecondVictoryCheck(out int ResultII) && EndGame)
            {
                StartCoroutine(EndGameProcess(ResultII));
                yield break;
            }

            Revive();
            Waiting = true;

            // Coin && AI
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
            for (int i = 0; i < Groups.Count; i++)
            {
                if (!Groups[i] || !Groups[i].GetAIControl())
                    continue;
                if (Groups[i].Side == 0)
                {
                    if (Result == 1)
                        Groups[i].GetAIControl().Execute(CurrentTurn, true);
                    else
                        Groups[i].GetAIControl().Execute(CurrentTurn, false);
                }
                else if (Groups[i].Side == 1)
                {
                    if (Result == -1)
                        Groups[i].GetAIControl().Execute(CurrentTurn, true);
                    else
                        Groups[i].GetAIControl().Execute(CurrentTurn, false);
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

        public bool SecondVictoryCheck(out int Result)
        {
            for (int i = 0; i < Cores.Count; i++)
            {
                if (Cores[i].GetLife() <= 0 && Cores[i].GetSide() == 0)
                {
                    Result = -1;
                    return true;
                }
                else if (Cores[i].GetLife() <= 0 && Cores[i].GetSide() == 1)
                {
                    Result = 1;
                    return true;
                }
            }
            Result = 0;
            return false;
        }

        public IEnumerator EndGameProcess(int Result)
        {
            KeyBase.Main.SetKey("LastResult", Result);
            yield return new WaitForSeconds(0.5f);
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
        }

        public void Revive()
        {
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
                Cards[i].Revive();
            }
        }

        public void AddItem(GameObject ItemPrefab, float CoinChange, CardGroup Group)
        {
            if (!Group || !ItemPrefab)
                return;
            Mark_Skill S = ItemPrefab.GetComponent<Mark_Skill>();
            ChangeCoin(CoinChange);
            foreach (Card C in Group.Cards)
                C.AddSkill(S);
        }

        public void RemoveItem(GameObject ItemPrefab, float CoinChange, CardGroup Group)
        {
            if (!Group || !ItemPrefab)
                return;
            Mark_Skill S = ItemPrefab.GetComponent<Mark_Skill>();
            ChangeCoin(CoinChange);
            foreach (Card C in Group.Cards)
                C.RemoveSkill(S.GetID(), 1);
        }

        public void AddItem(GameObject ItemPrefab, CardGroup Group)
        {
            if (!Group || !ItemPrefab)
                return;
            Mark_Skill S = ItemPrefab.GetComponent<Mark_Skill>();
            foreach (Card C in Group.Cards)
                C.AddSkill(S);
        }

        public void RemoveItem(GameObject ItemPrefab, CardGroup Group)
        {
            if (!Group || !ItemPrefab)
                return;
            Mark_Skill S = ItemPrefab.GetComponent<Mark_Skill>();
            foreach (Card C in Group.Cards)
                C.RemoveSkill(S.GetID(), 1);
        }

        public void ConsumeItem(GameObject ItemPrefab, CardGroup Group, Card Source, float Count)
        {
            if (!Group)
                return;
            Mark_Skill S = ItemPrefab.GetComponent<Mark_Skill>();
            foreach (Card C in Group.Cards)
                if (C != Source)
                    C.RemoveSkill(S.GetID(), (int)Count);
        }

        public Card GetCurrentMC()
        {
            return MCGroup.GetCurrentCard();
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