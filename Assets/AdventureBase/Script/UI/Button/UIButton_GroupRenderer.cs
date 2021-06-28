using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ESP;

namespace ADV
{
    public class UIButton_GroupRenderer : UIButton_Square {
        public CardRenderer CR;
        public GameObject Selection;

        public override void Update()
        {
            if (GetGroup() && CombatControl.Main.SelectintGroup == GetGroup())
                Selection.SetActive(true);
            else
                Selection.SetActive(false);
            base.Update();
        }

        public override void MouseDownEffect()
        {
            if (CombatControl.Main.SelectintGroup == GetGroup())
                CombatControl.Main.SelectintGroup = null;
            else if (GetGroup() && GetGroup().Side == 0 && GetGroup().GetAIControl() && GetGroup().GetAIControl().GetType() == typeof(AIControl_Friendly))
            {
                if (GetGroup() == CombatControl.Main.MCGroup)
                    CombatControl.Main.SelectintGroup = null;
                else
                    CombatControl.Main.SelectintGroup = GetGroup();
            }
            base.MouseDownEffect();
        }

        public CardGroup GetGroup()
        {
            if (!CR || !CR.GetTarget())
                return null;
            return CR.GetTarget().GetGroup();
        }
    }
}