using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_ReturnKey : Mark_Status_Mod {
        public List<GameObject> SignalPrefabs;
        public List<string> ReturnKeys;
        public List<string> OutputKeys;

        public override void ReturnSignal(Signal S)
        {
            if (Trigger(S))
            {
                for (int i = 0; i < SignalPrefabs.Count; i++)
                {
                    KeyBase KB = gameObject.AddComponent<KeyBase>();
                    KB.Ini();
                    for (int j = 0; j < ReturnKeys.Count; j++)
                        KB.SetKey(OutputKeys[j], S.GetKey(ReturnKeys[j]));
                    SendSignal(SignalPrefabs[i], KB.Keys);
                }
                Source.RemoveStatus(this);
            }
            base.ReturnSignal(S);
        }

        public override bool Trigger(Signal S)
        {
            bool a = false;
            foreach (string s in ReturnKeys)
                if (S.HasKey(s))
                    a = true;
            return base.Trigger(S) && a;
        }
    }
}