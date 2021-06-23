using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class EventStep_NextLevel : EventStep {

        public override void OnEffect()
        {
            LevelControl.Main.NextLevel();
            ThreadControl.Main.NextStep();
        }
    }
}