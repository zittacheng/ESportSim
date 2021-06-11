using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class CombatControl : MonoBehaviour {
        public static CombatControl Main;
        public float TimeScale = -1;
        [Space]
        public Card SelectingCard;
        public Mark SelectingMark;
        public Vector2 SelectingPosition;
        [Space]
        public List<Card> Cards;
        [HideInInspector] public List<Card> DeathCards;
        public List<Medium> Mediums;

        public void Awake()
        {

        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // Temp
            if (Input.GetKeyDown(KeyCode.S) && TimeScale == -1)
            {
                StartOfCombat();
                TimeScale = 1;
            }
        }

        public void StartOfCombat()
        {
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
                if (Cards[i].CombatActive())
                    Cards[i].StartOfCombat();
            }
        }

        public void EndOfCombat()
        {
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
                if (Cards[i].CombatActive())
                    Cards[i].EndOfCombat();
            }
            for (int i = Mediums.Count - 1; i >= 0; i--)
                Mediums[i].EndEffect();
        }

        public void Revive()
        {
            for (int i = Cards.Count - 1; i >= 0; i--)
            {
                if (!Cards[i].CombatActive())
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