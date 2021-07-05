using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class UIButton_CVContentField : UIButton_Square {
        public ConversationContentRenderer CCR;

        public override void MouseEnterEffect()
        {
            CCR.MouseOn = true;
            base.MouseEnterEffect();
        }

        public override void MouseExitEffect()
        {
            CCR.MouseOn = false;
            base.MouseExitEffect();
        }
    }
}