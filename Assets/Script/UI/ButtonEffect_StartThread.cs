using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class ButtonEffect_StartThread : ButtonEffect {

        public override void Effect()
        {
            if (!ThreadControl.Main.CanStartProcess())
                return;
            SubUIControl.Main.CloseWindow();
            ThreadControl.Main.StartProcess();
        }
    }
}