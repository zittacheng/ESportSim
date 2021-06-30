using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class UIButton_ItemBaseSwitch : UIButton_Square {
        public Animator Anim;

        public override void MouseDownEffect()
        {
            if (!gameObject.activeInHierarchy)
                return;
            Anim.SetBool("ItemBaseActive", !Anim.GetBool("ItemBaseActive"));
            base.MouseDownEffect();
        }
    }
}