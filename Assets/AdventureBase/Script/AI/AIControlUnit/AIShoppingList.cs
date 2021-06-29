using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class AIShoppingList : AIControlUnit {
        public List<GameObject> List;

        public override void Execute(CardGroup Source, bool Victory)
        {
            if (Source.GetAIControl() && Source.GetAIControl().GetComponent<AIControl_CoinBased>())
            {
                AIControl_CoinBased AC = (AIControl_CoinBased)Source.GetAIControl();
                GameObject G = List[Random.Range(0, List.Count)];
                if (G)
                    AC.Buy(G);
            }
            base.Execute(Source, Victory);
        }
    }
}