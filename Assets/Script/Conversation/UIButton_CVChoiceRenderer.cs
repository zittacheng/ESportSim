using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class UIButton_CVChoiceRenderer : UIButton_Square {
        public CVChoiceRenderer CVCR;

        public override void MouseEnterEffect()
        {
            CVCR.MouseOn = true;
            base.MouseEnterEffect();
        }

        public override void MouseExitEffect()
        {
            CVCR.MouseOn = false;
            base.MouseExitEffect();
        }

        public override void MouseDownEffect()
        {
            CVCR.Select();
            base.MouseDownEffect();
        }
    }
}