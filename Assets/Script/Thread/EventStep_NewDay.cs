using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class EventStep_NewDay : EventStep {

        public override void OnEffect()
        {
            ThreadControl.Main.NextStep();
            GlobalControl.Main.NewDay();
        }
    }
}