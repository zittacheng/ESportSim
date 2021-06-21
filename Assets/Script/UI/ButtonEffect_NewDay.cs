using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class ButtonEffect_NewDay : ButtonEffect {

        public override void Effect()
        {
            ThreadControl.Main.ProcessAdvance();
            base.Effect();
        }
    }
}