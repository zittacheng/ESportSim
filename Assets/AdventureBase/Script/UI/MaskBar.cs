using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class MaskBar : EnergyBar {
        public GameObject Mask;
        public Vector2 MaskRange;
        
        public override void Render(float Value)
        {
            float a = Value;
            if (a < 0)
                a = 0;
            if (a > 1)
                a = 1;
            CurrentValue = a;
            Mask.transform.localPosition = new Vector3(MaskRange.x + (MaskRange.y - MaskRange.x) * a, Mask.transform.localPosition.y, Mask.transform.localPosition.z);
            Right.transform.localPosition = new Vector3(RightPositionRange.x + (RightPositionRange.y - RightPositionRange.x) * a, 
                Right.transform.localPosition.y, Right.transform.localPosition.z);
        }
    }
}