using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class AIControlUnit : MonoBehaviour {
        public List<GameObject> VictoryPrefabs;
        public List<GameObject> DefeatPrefabs;
        public AIControlUnit NextUnit;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Execute(Card Source, bool Victory)
        {
            if (Victory)
            {
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
                if (DefeatPrefabs.Count <= 0)
                {

                    if (NextUnit)
                        NextUnit.Execute(Source, Victory);
                    return;
                }
                else
                {
                    GameObject G = DefeatPrefabs[Random.Range(0, VictoryPrefabs.Count)];
                    CombatControl.Main.AddItem(G, Source);
                    if (NextUnit)
                        NextUnit.Execute(Source, Victory);
                    return;
                }
            }
        }
    }
}