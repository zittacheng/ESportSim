using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class ButtonEffect_StartThread : ButtonEffect {

        public override void Effect()
        {
            if (!ThreadControl.Main.CanStartProcess())
                return;
            UIControl.Main.CloseWindow();
            ThreadControl.Main.StartProcess();
        }
    }
}