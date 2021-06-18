using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class UIButton_Undo : UIButton_Square {

        public override void MouseDownEffect()
        {
            UndoControl.Main.Undo();
            base.MouseDownEffect();
        }
    }
}