using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_OnDamaged_SOT : Mark_Status_OnDamaged {
        public List<GameObject> EndPrefabs;

        public void StartOfTurn()
        {
            for (int i = 0; i < EndPrefabs.Count; i++)
                SendSignal(EndPrefabs[i]);
            //base.StartOfTurn();
        }
    }
}