using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class AIControlUnit_General : AIControlUnit {
        public List<GameObject> VictoryPrefabs;
        public List<GameObject> DefeatPrefabs;
        [Space]
        public List<string> VictoryCards;
        public List<string> DefeatCards;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void Execute(CardGroup Source, bool Victory)
        {
            // Item
            if (Victory)
            {
                if (VictoryPrefabs.Count > 0)
                {
                    GameObject G = VictoryPrefabs[Random.Range(0, VictoryPrefabs.Count)];
                    CombatControl.Main.AddItem(G, Source);
                }
            }
            else
            {
                if (DefeatPrefabs.Count > 0)
                {
                    GameObject G = DefeatPrefabs[Random.Range(0, DefeatPrefabs.Count)];
                    CombatControl.Main.AddItem(G, Source);
                }
            }

            // Hero
            if (Victory)
            {
                if (VictoryCards.Count > 0)
                {
                    string s = VictoryCards[Random.Range(0, VictoryCards.Count)];
                    Source.SwitchCard(s);
                }
            }
            else
            {
                if (DefeatCards.Count > 0)
                {
                    string s = DefeatCards[Random.Range(0, DefeatCards.Count)];
                    Source.SwitchCard(s);
                }
            }

            base.Execute(Source, Victory);
        }
    }
}