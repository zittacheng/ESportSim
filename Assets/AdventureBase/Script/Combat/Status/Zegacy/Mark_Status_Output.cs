using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Output : Mark_Status_Mod {
        public List<GameObject> SignalPrefabs;

        public override void OutputSignal(Signal S)
        {
            if (Trigger(S))
                for (int i = 0; i < SignalPrefabs.Count; i++)
                    SendSignal(SignalPrefabs[i]);
            base.OutputSignal(S);
        }
    }
}