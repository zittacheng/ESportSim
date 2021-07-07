using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class UIButton_DialogueRenderer : UIButton_Square {

        public override void MouseDownEffect()
        {
            DialogueControl.Main.Advance();
            base.MouseDownEffect();
        }
    }
}