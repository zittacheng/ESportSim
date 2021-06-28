using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class Mark_Status_Poison : Mark_Status_EndOfTurn {

        public override void Stack(Mark_Status M)
        {
            if (M.HasKey("Stack") && M.GetComponent<Mark_Status_Poison>())
            {
                Mark_Status_Poison MP = M.GetComponent<Mark_Status_Poison>();
                ChangeKey("Stack", M.GetKey("Stack"));
                for (int i = 0; i < MP.SignalPrefabs.Count; i++)
                    SignalPrefabs.Add(MP.SignalPrefabs[i]);
            }
            if (M.HasKey("Duration") && M.GetComponent<Mark_Status_Poison>())
            {
                Mark_Status_Poison MP = M.GetComponent<Mark_Status_Poison>();
                ChangeKey("Duration", M.GetKey("Duration"));
            }
            base.Stack(M);
        }
    }
}