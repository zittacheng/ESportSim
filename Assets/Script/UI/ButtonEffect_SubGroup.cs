using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class ButtonEffect_SubGroup : ButtonEffect {
        public UIWindow_SubGroup Window;
        public GameObject Group;

        public override void Effect()
        {
            Window.SwitchGroup(Group);
            base.Effect();
        }
    }
}