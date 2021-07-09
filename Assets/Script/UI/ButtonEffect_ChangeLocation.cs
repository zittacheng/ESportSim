using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class ButtonEffect_ChangeLocation : ButtonEffect {
        public Location Target;

        public override void Effect()
        {
            if (Target == Location.Shop)
                SubUIControl.Main.MoveToShp();
            else if (Target == Location.Common)
                SubUIControl.Main.MoveBack();
            base.Effect();
        }
    }
}