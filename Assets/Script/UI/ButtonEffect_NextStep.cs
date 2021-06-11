using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class ButtonEffect_NextStep : ButtonEffect {

        public override void Effect()
        {
            ThreadControl.Main.NextStep();
        }
    }
}