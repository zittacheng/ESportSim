using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class ButtonEffect_NextEvent : ButtonEffect {

        public override void Effect()
        {
            ThreadControl.Main.EndEvent();
        }
    }
}