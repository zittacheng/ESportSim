using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class AIControlUnit_AddItem : AIControlUnit {
        public List<GameObject> VictoryPrefabs;
        public List<GameObject> DefeatPrefabs;

        public override void Execute(CardGroup Source, bool Victory)
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