using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Shield_Coversion : Mark_Status_Shield {
        public List<GameObject> SignalPrefabs;

        public override float ProcessLifeChange(float LifeChange)
        {
            if (LifeChange >= 0)
                return LifeChange;

            for (int i = 0; i < SignalPrefabs.Count; i++)
            {
                KeyBase KB = gameObject.AddComponent<KeyBase>();
                KB.Ini();
                KB.SetKey("Heal", -LifeChange);
                //SendSignal(SignalPrefabs[i], KB);
            }

            Break();
            return 0;
        }
    }
}