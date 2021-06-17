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
                CombatControl.Main.AddItem(CombatControl.Main.SelectingItem.gameObject, -CombatControl.Main.SelectingItem.GetKey("Cost"));
            else if (!Buy && CanSell())
                CombatControl.Main.RemoveItem(CombatControl.Main.SelectingItem.gameObject, CombatControl.Main.SelectingItem.GetKey("Cost") * 0.4f);
            base.MouseDownEffect();
        }

        public bool CanBuy()
        {
            if (!CombatControl.Main.Waiting)
                return false;
            Mark_Skill S = CombatControl.Main.SelectingItem;
            if (S.GetKey("CanStack") == 0 && CombatControl.Main.CurrentMC.GetSkill(S.GetID(), out _))
                return false;
            return S && !S.Source && CombatControl.Main.Coin >= S.GetKey("Cost");
        }

        public bool CanSell()
        {
            if (!CombatControl.Main.Waiting)
                return false;
            Mark_Skill S = CombatControl.Main.SelectingItem;
            return S && S.Source && S.Source == CombatControl.Main.CurrentMC && S.GetKey("Count") > 0;
        }
    }
}