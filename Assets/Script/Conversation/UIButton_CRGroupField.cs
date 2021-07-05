using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class UIButton_CRGroupField : UIButton_Square {
        public ConversationRendererGroup CRG;

        public override void MouseEnterEffect()
        {
            CRG.MouseOn = true;
            base.MouseEnterEffect();
        }

        public override void MouseExitEffect()
        {
            CRG.MouseOn = false;
            base.MouseExitEffect();
        }
    }
}