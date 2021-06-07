using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESP
{
    public class UIButton_Circle : UIButton {
        public float CursorRange;

        public override bool InRange()
        {
            Vector2 P = Cursor.Main.GetPosition();
            return (P - GetPosition()).magnitude <= CursorRange;
        }
    }
}