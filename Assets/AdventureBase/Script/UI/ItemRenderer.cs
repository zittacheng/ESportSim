using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ADV
{
    public class ItemRenderer : UIButton_Square {
        public CardRenderer CR;
        public GameObject ItemPrefab;
        public int Index;
        public TextMeshPro NameText;
        public TextMeshPro CountText;
        public TextMeshPro CostText;
        public GameObject SelectionBase;
        public SpriteRenderer ItemSprite;
        public SpriteRenderer EmptySprite;
        public float Alpha = 1;
        [Space]
        public GameObject PanelPivot;
        public PanelDirection PDirection;

        public override void Update()
        {
            Render();
            base.Update();
        }

        public Card GetTarget()
        {
            if (!CR)
                return CombatControl.Main.GetCurrentMC();
            return CR.GetTarget();
        }

        public GameObject GetItem()
        {
            if (ItemPrefab)
                return ItemPrefab;
            else if (GetTarget() && GetTarget().GetRenderSkill(Index))
                return GetTarget().GetRenderSkill(Index).gameObject;
            return null;
        }

        public void Render()
        {
            Card C = GetTarget();
            Mark_Skill S = null;
            if (GetItem())
                S = GetItem().GetComponent<Mark_Skill>();

            if (Alpha < 1 && CombatControl.Main.SelectingItemRenderer == this)
            {
                CombatControl.Main.SelectingItem = null;
                CombatControl.Main.SelectingItemRenderer = null;
            }

            if (!C || !S)
            {
                if (NameText)
                    NameText.text = "";
                if (CostText)
                    CostText.text = "";
                if (CountText)
                    CountText.text = "";
                if (ItemSprite)
                    ItemSprite.gameObject.SetActive(false);
                if (SelectionBase)
                    SelectionBase.SetActive(false);
                if (EmptySprite)
                {
                    EmptySprite.gameObject.SetActive(true);
                    EmptySprite.color = new Color(EmptySprite.color.r, EmptySprite.color.g, EmptySprite.color.b, Alpha);
                }
                return;
            }

            if (NameText)
            {
                NameText.text = S.GetName();
                NameText.color = new Color(NameText.color.r, NameText.color.g, NameText.color.b, Alpha);
            }

            Mark_Skill RS = GetTarget().GetSkill(S.GetID(), out _);

            if (CountText)
            {
                if (RS)
                    CountText.text = RS.GetKey("Count").ToString();
                CountText.color = new Color(CountText.color.r, CountText.color.g, CountText.color.b, Alpha);
            }

            if (CostText)
            {
                CostText.text = S.GetKey("Cost").ToString();
                CostText.color = new Color(CostText.color.r, CostText.color.g, CostText.color.b, Alpha);
            }

            if (SelectionBase)
                SelectionBase.SetActive(CombatControl.Main.SelectingItemRenderer == this);

            if (ItemSprite)
            {
                ItemSprite.gameObject.SetActive(true);
                if (RS)
                    ItemSprite.sprite = RS.GetIcon();
                else if (S)
                    ItemSprite.sprite = S.GetIcon();
                else
                    ItemSprite.sprite = null;
                ItemSprite.color = new Color(ItemSprite.color.r, ItemSprite.color.g, ItemSprite.color.b, Alpha);
            }

            if (EmptySprite)
                EmptySprite.gameObject.SetActive(false);
        }

        public bool CanInteract()
        {
            if (Alpha < 1)
                return false;
            if (CombatControl.Main.SelectingItemRenderer != this)
            {
                if (!GetTarget() || !GetItem() || !CombatControl.Main.Waiting)
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
            if (CombatControl.Main.SelectingItemRenderer != this)
            {
                CombatControl.Main.SelectingItem = GetItem().GetComponent<Mark_Skill>();
                CombatControl.Main.SelectingItemRenderer = this;
            }
            else
            {
                CombatControl.Main.SelectingItem = null;
                CombatControl.Main.SelectingItemRenderer = null;
            }

            CombatControl.Main.SelectingSwitch = null;
            CombatControl.Main.SelectingCard = null;
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
            if (Alpha <= 0)
                return;
            if (!GetItem())
            {
                UIControl.Main.ItemPanel.Render(new Vector2(), 0, null);
                return;
            }
            Mark_Skill S = GetItem().GetComponent<Mark_Skill>();
            if (!S || !S.GetInfo())
            {
                UIControl.Main.ItemPanel.Render(new Vector2(), 0, null);
                return;
            }

            UIControl.Main.ItemPanel.Render(PanelPivot.transform.position, PDirection, S.GetInfo());
            base.MouseEnterEffect();
        }

        public override void MouseExitEffect()
        {
            UIControl.Main.ItemPanel.Render(new Vector2(), 0, null);
            base.MouseExitEffect();
        }

        public override void DoubleClickEffect()
        {
            CardGroup CG = CombatControl.Main.SelectintGroup;
            UIButton_Buy B = UIControl.Main.BuyButton;
            if (B.CanBuy(CG) || B.CanSell(CG) || B.CanSwitch(CG))
                B.MouseDownEffect();
            B = UIControl.Main.SellButton;
            if (B.CanBuy(CG) || B.CanSell(CG) || B.CanSwitch(CG))
                B.MouseDownEffect();
            base.DoubleClickEffect();
        }
    }
}