using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class AIControlUnit_General : AIControlUnit {
        public List<GameObject> VictoryPrefabs;
        public List<GameObject> DefeatPrefabs;
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
            if (Victory)
            {
                if (VictoryCards.Count > 0)
                {
                    string s = VictoryCards[Random.Range(0, VictoryCards.Count)];
                    Source.SwitchCard(s);
                }

                if (VictoryPrefabs.Count <= 0)
                {
                    if (NextUnit)
                        NextUnit.Execute(Source, Victory);
                    return;
                }
                else
                {
                    GameObject G = VictoryPrefabs[Random.Range(0, VictoryPrefabs.Count)];
                    CombatControl.Main.AddItem(G, Source);
                    if (NextUnit)
                        NextUnit.Execute(Source, Victory);
                    return;
                }
            }
            else
            {
                if (DefeatCards.Count > 0)
                {
                    string s = DefeatCards[Random.Range(0, DefeatCards.Count)];
                    Source.SwitchCard(s);
                }

                if (DefeatPrefabs.Count <= 0)
                {
                    if (NextUnit)
                        NextUnit.Execute(Source, Victory);
                    return;
                }
                else
                {
                    GameObject G = DefeatPrefabs[Random.Range(0, DefeatPrefabs.Count)];
                    CombatControl.Main.AddItem(G, Source);
                    if (NextUnit)
                        NextUnit.Execute(Source, Victory);
                    return;
                }
            }
        }
    }
}