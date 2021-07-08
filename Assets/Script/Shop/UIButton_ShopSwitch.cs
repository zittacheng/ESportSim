using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class UIButton_ShopSwitch : UIButton_Square {
        public ShopSwitch Switch;

        public override void MouseDownEffect()
        {
            if (gameObject.activeInHierarchy)
            {
                Switch.Interact();
                base.MouseDownEffect();
            }
        }
    }
}