using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_EndOfTurn_Final : Mark_Status {
        public List<GameObject> SignalPrefabs;

        public void EndOfTurn()
        {
            if (!HasKey("Duration") || GetKey("Duration") <= 1)
                for (int i = 0; i < SignalPrefabs.Count; i++)
                    SendSignal(SignalPrefabs[i]);
            //base.EndOfTurn();
        }
    }
}
