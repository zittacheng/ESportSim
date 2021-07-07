using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class UIButton_DialogueCR : UIButton_Square {
        public DialogueChoiceRenderer DCR;

        public override void MouseEnterEffect()
        {
            DCR.MouseOn = true;
            base.MouseEnterEffect();
        }

        public override void MouseExitEffect()
        {
            DCR.MouseOn = false;
            base.MouseExitEffect();
        }

        public override void MouseDownEffect()
        {
            if (gameObject.activeInHierarchy)
                DCR.Interact();
            base.MouseDownEffect();
        }
    }
}