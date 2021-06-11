using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class ButtonEffect_CloseWindow : ButtonEffect {

        public override void Effect()
        {
            SubUIControl.Main.CloseWindow();
        }
    }
}