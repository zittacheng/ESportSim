using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class UIButton_InventorySwitch : UIButton_Square {
        public GameObject Target;
        public GameObject Ori;
        public Vector2 TargetPosition;
        public Vector2 OriPosition;

        public override void MouseDownEffect()
        {
            Target.transform.localPosition = new Vector3(TargetPosition.x, TargetPosition.y, Target.transform.localPosition.z);
            Ori.transform.localPosition = new Vector3(OriPosition.x, OriPosition.y, Ori.transform.localPosition.z);
            if (Ori.GetComponent<Inventory>())
                Ori.GetComponent<Inventory>().StartIndex = 0;
            base.MouseDownEffect();
        }
    }
}