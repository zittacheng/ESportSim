using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class AIControlUnit_AddItem : AIControlUnit {
        public List<GameObject> Items;

        public override void Execute(CardGroup Source, bool Victory)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                GameObject G = Items[i];
                CombatControl.Main.AddItem(G, Source);
            }
            base.Execute(Source, Victory);
        }
    }
}