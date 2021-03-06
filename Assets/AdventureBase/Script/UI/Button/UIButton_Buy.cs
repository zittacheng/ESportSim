using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ADV
{
    public class UIButton_Buy : UIButton_Square {
        public bool Buy;

        public override void MouseDownEffect()
        {
            if (!CombatControl.Main.SelectintGroup)
            {
                if (CanSwitch() && CombatControl.Main.MCGroup.CanSwitch(CombatControl.Main.SelectingSwitch.Key))
                {
                    UndoControl.Main.NewUnit(null, null, 0, CombatControl.Main.GetCurrentMC().GetInfo().GetID());
                    CombatControl.Main.MCGroup.SwitchCard(CombatControl.Main.SelectingSwitch.Key);
                }
                else if (CanBuy())
                {
                    UndoControl.Main.NewUnit(null, CombatControl.Main.SelectingItem.gameObject, CombatControl.Main.SelectingItem.GetKey("Cost"), "");
                    CombatControl.Main.AddItem(CombatControl.Main.SelectingItem.gameObject, -CombatControl.Main.SelectingItem.GetKey("Cost"), CombatControl.Main.MCGroup);
                }
                else if (CanSell())
                {
                    UndoControl.Main.NewUnit(CombatControl.Main.SelectingItem.gameObject, null, -CombatControl.Main.SelectingItem.GetKey("Cost") * 0.4f, "");
                    CombatControl.Main.RemoveItem(CombatControl.Main.SelectingItem.gameObject, CombatControl.Main.SelectingItem.GetKey("Cost") * 0.4f, CombatControl.Main.MCGroup);
                }
            }
            else
            {
                CardGroup CG = CombatControl.Main.SelectintGroup;
                AIControl_Friendly AF = (AIControl_Friendly)CG.GetAIControl();
                if (!AF)
                    return;
                if (CanSwitch(CG) && CG.CanSwitch(CombatControl.Main.SelectingSwitch.Key))
                    AF.ForceSwitch(CombatControl.Main.SelectingSwitch.Key, out _);
                else if (CanBuy(CG))
                    AF.ForceBuy(CombatControl.Main.SelectingItem.gameObject, out _);
                else if (CanSell(CG))
                    AF.ForceSell(CombatControl.Main.SelectingItem.gameObject, out _);
            }
            base.MouseDownEffect();
        }

        public bool CanBuy()
        {
            if (!Buy)
                return false;
            if (!CombatControl.Main.Waiting)
                return false;
            Mark_Skill S = CombatControl.Main.SelectingItem;
            if (!S || (S.GetKey("CanStack") == 0 && CombatControl.Main.GetCurrentMC().GetSkill(S.GetID(), out _)))
                return false;
            return !S.Source && CombatControl.Main.Coin >= S.GetKey("Cost");
        }

        public bool CanSell()
        {
            if (Buy)
                return false;
            if (!CombatControl.Main.Waiting)
                return false;
            Mark_Skill S = CombatControl.Main.SelectingItem;
            return S && S.Source && S.Source == CombatControl.Main.GetCurrentMC() && CombatControl.Main.GetCurrentMC().Skills.Contains(S) && S.GetKey("Count") > 0;
        }

        public bool CanSwitch()
        {
            if (!Buy)
                return false;
            if (!CombatControl.Main.Waiting)
                return false;
            return CombatControl.Main.SelectingSwitch && CombatControl.Main.GetCurrentMC().GetInfo().GetID() != CombatControl.Main.SelectingSwitch.Key;
        }

        public bool CanBuy(CardGroup CG)
        {
            if (!Buy)
                return false;
            if (!CG)
                return CanBuy();
            if (!CombatControl.Main.Waiting)
                return false;
            AIControl_Friendly AI = (AIControl_Friendly)CG.GetAIControl();
            if (!AI || !AI.CanBuy)
                return false;
            Mark_Skill S = CombatControl.Main.SelectingItem;
            if (!S || (S.GetKey("CanStack") == 0 && CG.GetCurrentCard().GetSkill(S.GetID(), out _)))
                return false;
            return !S.Source && AI.Coin >= S.GetKey("Cost");
        }

        public bool CanSell(CardGroup CG)
        {
            if (Buy)
                return false;
            if (!CG)
                return CanSell();
            if (!CombatControl.Main.Waiting)
                return false;
            AIControl_Friendly AI = (AIControl_Friendly)CG.GetAIControl();
            if (!AI || !AI.CanSell)
                return false;
            Mark_Skill S = CombatControl.Main.SelectingItem;
            return S && S.Source && S.Source == CG.GetCurrentCard() && CG.GetCurrentCard().Skills.Contains(S) && S.GetKey("Count") > 0;
        }

        public bool CanSwitch(CardGroup CG)
        {
            if (!Buy)
                return false;
            if (!CG)
                return CanSwitch();
            if (!CombatControl.Main.Waiting)
                return false;
            AIControl_Friendly AI = (AIControl_Friendly)CG.GetAIControl();
            if (!AI || !AI.CanSwitch)
                return false;
            return CombatControl.Main.SelectingSwitch;
        }
    }
}