using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class UIButton_Buy : UIButton_Square {
        public bool Buy;

        public override void MouseDownEffect()
        {
            if (Buy && CanBuy())
                CombatControl.Main.AddItem(CombatControl.Main.SelectingItem.gameObject, -CombatControl.Main.SelectingItem.GetKey("Cost"), CombatControl.Main.MCGroup);
            else if (!Buy && CanSell())
                CombatControl.Main.RemoveItem(CombatControl.Main.SelectingItem.gameObject, CombatControl.Main.SelectingItem.GetKey("Cost") * 0.4f, CombatControl.Main.MCGroup);
            base.MouseDownEffect();
        }

        public bool CanBuy()
        {
            if (!CombatControl.Main.Waiting)
                return false;
            Mark_Skill S = CombatControl.Main.SelectingItem;
            if (S.GetKey("CanStack") == 0 && CombatControl.Main.GetCurrentMC().GetSkill(S.GetID(), out _))
                return false;
            return S && !S.Source && CombatControl.Main.Coin >= S.GetKey("Cost");
        }

        public bool CanSell()
        {
            if (!CombatControl.Main.Waiting)
                return false;
            Mark_Skill S = CombatControl.Main.SelectingItem;
            return S && S.Source && S.Source == CombatControl.Main.GetCurrentMC() && S.GetKey("Count") > 0;
        }
    }
}