using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_RecklessStrike : Mark_Status_Mod {
        public List<GameObject> SignalPrefabs;

        public override void ReturnSignal(Signal S)
        {
            if (Trigger(S) && S.HasKey("Damage"))
            {
                for (int i = 0; i < SignalPrefabs.Count; i++)
                {
                    KeyBase KB = gameObject.AddComponent<KeyBase>();
                    KB.Ini();
                    KB.SetKey("Damage", S.GetKey("Damage"));
                    SendSignal(SignalPrefabs[i], KB.Keys);
                }
                Source.RemoveStatus(this);
            }
            base.ReturnSignal(S);
        }
    }
}