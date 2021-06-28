using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class UIButton_StartButton : UIButton_Square {
        public GameObject AnimBase;

        public override void Update()
        {
            if (CombatControl.Main.CanStartCombat())
                AnimBase.SetActive(true);
            else
                AnimBase.SetActive(false);
            base.Update();
        }

        public override void MouseDownEffect()
        {
            if (CombatControl.Main.CanStartCombat())
                CombatControl.Main.StartOfCombat();
            base.MouseDownEffect();
        }
    }
}