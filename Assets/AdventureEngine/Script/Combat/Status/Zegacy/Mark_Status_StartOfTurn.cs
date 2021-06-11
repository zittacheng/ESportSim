using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_StartOfTurn : Mark_Status {
        public List<GameObject> SignalPrefabs;

        public void StartOfTurn()
        {
            for (int i = 0; i < SignalPrefabs.Count; i++)
                SendSignal(SignalPrefabs[i]);
            //base.StartOfTurn();
        }
    }
}