using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Shield_EndDamage : Mark_Status_Shield {
        public List<GameObject> SignalPrefabs;

        public void StartOfTurn()
        {
            for (int i = 0; i < SignalPrefabs.Count; i++)
            {
                KeyBase KB = gameObject.AddComponent<KeyBase>();
                KB.Ini();
                KB.SetKey("Damage", GetKey("Shield"));
                SendSignal(SignalPrefabs[i], KB.Keys);
            }
            //base.StartOfTurn();
        }
    }
}