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
            Target.transform.position = new Vector3(TargetPosition.x, TargetPosition.y, Target.transform.position.z);
            Ori.transform.position = new Vector3(OriPosition.x, OriPosition.y, Ori.transform.position.z);
            if (Ori.GetComponent<Inventory>())
                Ori.GetComponent<Inventory>().StartIndex = 0;
            base.MouseDownEffect();
        }
    }
}