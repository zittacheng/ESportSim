using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class UIButton_ConversationRenderer : UIButton_Square {
        public ConversationRenderer CVR;

        public override void MouseDownEffect()
        {
            CVR.Select();
            base.MouseDownEffect();
        }
    }
}