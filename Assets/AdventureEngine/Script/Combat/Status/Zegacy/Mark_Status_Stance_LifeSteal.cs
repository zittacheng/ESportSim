using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Stance_LifeSteal : Mark_Status_Mod {
        public List<GameObject> SignalPrefabs;

        public override void ReturnSignal(Signal S)
        {
            if (Trigger(S) && S.HasKey("Damage"))
            {
                for (int i = 0; i < SignalPrefabs.Count; i++)
                {
                    KeyBase KB = gameObject.AddComponent<KeyBase>();
                    KB.Ini();
                    KB.SetKey("Heal", S.GetKey("Damage"));
                    SendSignal(SignalPrefabs[i], KB.Keys);
                }
                Source.RemoveStatus(this);
            }
            base.ReturnSignal(S);
        }

        public void EndOfTurn()
        {
            Source.RemoveStatus(this);
            //base.EndOfTurn();
        }

        public override void CommonKeys()
        {
            // "LifeSteal": Heal rate
            base.CommonKeys();
        }
    }
}