using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class UIButton_Card : UIButton_Circle {
        public Card Source;

        public override void MouseDownEffect()
        {
            Source.GetAnim().MouseDown();
            base.MouseDownEffect();
        }

        public override void MouseUpEffect()
        {
            Source.GetAnim().MouseUp();
            base.MouseUpEffect();
        }
    }
}