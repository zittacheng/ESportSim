using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ADV;

namespace ESP
{
    public class ButtonEffect_EventRenderer : ButtonEffect {
        public EventRenderer ER;

        public override void Effect()
        {
            ER.Interact();
        }
    }
}