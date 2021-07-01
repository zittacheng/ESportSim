using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class UIButton_Square : UIButton {
        public Vector2 CursorRangeX;
        public Vector2 CursorRangeY;

        public override bool InRange()
        {
            Vector2 Position = Cursor.Main.GetPosition();
            return Position.x > GetCursorRangeX().x && Position.x < GetCursorRangeX().y && Position.y > GetCursorRangeY().x && Position.y < GetCursorRangeY().y;
        }

        public Vector2 GetCursorRangeX()
        {
            return new Vector2(CursorRangeX.x * transform.lossyScale.x + GetPosition().x, CursorRangeX.y * transform.lossyScale.x + GetPosition().x);
        }

        public Vector2 GetCursorRangeY()
        {
            return new Vector2(CursorRangeY.x * transform.lossyScale.y + GetPosition().y, CursorRangeY.y * transform.lossyScale.y + GetPosition().y);
        }
    }
}