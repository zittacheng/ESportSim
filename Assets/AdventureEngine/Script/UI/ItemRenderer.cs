using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ADV
{
    public class ItemRenderer : UIButton_Square {
        //public Card Source;
        public CardRenderer CR;
        public GameObject ItemPrefab;
        public int Index;
        public TextMeshPro NameText;
        public TextMeshPro CountText;
        public TextMeshPro CostText;
        public GameObject SelectionBase;
        public SpriteRenderer ItemSprite;

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
                return;
            }

            if (NameText)
                NameText.text = S.GetName();

            Mark_Skill RS = GetTarget().GetSkill(S.GetID(), out _);

            if (CountText && RS)
                CountText.text = RS.GetKey("Count").ToString();

            if (CostText)
                CostText.text = S.GetKey("Cost").ToString();

            if (SelectionBase)
                SelectionBase.SetActive(CombatControl.Main.SelectingItemRenderer == this);

            if (ItemSprite)
                ItemSprite.gameObject.SetActive(true);
        }

        public bool CanInteract()
        {
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
        }

        public override void MouseDownEffect()
        {
            if (!CanInteract())
                return;
            Interact();
            base.MouseDownEffect();
        }
    }
}