using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ADV
{
    public class SwitchRenderer : UIButton_Square {
        public string Key;
        public TextMeshPro NameText;
        public GameObject SelectionBase;
        public GameObject PanelPivot;
        public PanelDirection PDirection;

        public override void Update()
        {
            //Render();
            SelectionBase.SetActive(CombatControl.Main.SelectingSwitch == this);
            base.Update();
        }

        public Card GetTarget()
        {
            return CombatControl.Main.MCGroup.GetCard(Key);
        }

        public void Render()
        {
            Card C = GetTarget();
            if (!C)
            {
                if (NameText)
                    NameText.text = "";
                if (SelectionBase)
                    SelectionBase.SetActive(false);
                return;
            }

            if (NameText)
                NameText.text = C.GetName();

            if (SelectionBase)
                SelectionBase.SetActive(CombatControl.Main.SelectingSwitch == this);
        }

        public bool CanInteract()
        {
            if (CombatControl.Main.SelectingItemRenderer != this)
            {
                if (!CombatControl.Main.Waiting)
                    return false;
                return true;
            }
            else
            {
                return true;
            }
        }

        public void Interact()
        {
            if (!CanInteract())
                return;
            if (CombatControl.Main.SelectingSwitch != this)
            {
                CombatControl.Main.SelectingCard = GetTarget();
                CombatControl.Main.SelectingSwitch = this;
            }
            else
            {
                CombatControl.Main.SelectingCard = null;
                CombatControl.Main.SelectingSwitch = null;
            }

            CombatControl.Main.SelectingItem = null;
            CombatControl.Main.SelectingItemRenderer = null;
        }

        public override void MouseDownEffect()
        {
            if (!CanInteract())
                return;
            Interact();
            base.MouseDownEffect();
        }

        public override void MouseEnterEffect()
        {
            base.MouseEnterEffect();
        }

        public override void MouseExitEffect()
        {
            base.MouseExitEffect();
        }

        public override void DoubleClickEffect()
        {
            CombatControl.Main.SelectingCard = GetTarget();
            CombatControl.Main.SelectingSwitch = this;
            CardGroup CG = CombatControl.Main.SelectintGroup;

            UIButton_Buy B = UIControl.Main.BuyButton;
            if (B.CanBuy(CG) || B.CanSell(CG) || B.CanSwitch(CG))
            {
                B.MouseDownEffect();
            }

            B = UIControl.Main.SellButton;
            if (B.CanBuy(CG) || B.CanSell(CG) || B.CanSwitch(CG))
            {
                B.MouseDownEffect();
            }

            base.DoubleClickEffect();
        }
    }
}