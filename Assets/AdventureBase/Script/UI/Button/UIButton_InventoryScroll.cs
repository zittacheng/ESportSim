using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class UIButton_InventoryScroll : UIButton_Square {
        public GameObject AnimBase;
        public Inventory Target;
        public int IndexChange;

        public override void Update()
        {
            if (CanInteract())
                AnimBase.SetActive(true);
            else
                AnimBase.SetActive(false);
            base.Update();
        }

        public bool CanInteract()
        {
            if (!Target)
                return false;
            if (IndexChange > 0)
                return Target.StartIndex < Target.IndexRange.y;
            if (IndexChange < 0)
                return Target.StartIndex > Target.IndexRange.x;
            return false;
        }

        public override void MouseDownEffect()
        {
            if (CanInteract())
                Target.StartIndex += IndexChange;
            base.MouseDownEffect();
        }
    }
}