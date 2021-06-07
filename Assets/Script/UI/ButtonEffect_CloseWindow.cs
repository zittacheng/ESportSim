using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class ButtonEffect_CloseWindow : ButtonEffect {

        public override void Effect()
        {
            UIControl.Main.CloseWindow();
        }
    }
}