﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ADV
{
    public class CardRenderer : UIElement {
        public int Side;
        public int Index;
        public Card DefaultTarget;
        public GameObject AnimBase;
        public TextMeshPro NameText;
        public int LifeRenderDirection;
        public EnergyBar LifeBar;
        public EnergyBar ManaBar;
        public TextMeshPro LifeText;
        public TextMeshPro LifeTextII;
        public TextMeshPro ManaText;
        public TextMeshPro ManaTextII;
        public TextMeshPro DamageText;
        public TextMeshPro AttackSpeedText;
        public TextMeshPro RecoverySpeedText;
        public List<StatusRenderer> StatusRenderers;

        public override void Render()
        {
            if (GetTarget())
                SetActive(true);
            else
                SetActive(false);
            MainRender();
            foreach (StatusRenderer SR in StatusRenderers)
                SR.RealUpdate(GetTarget());
            base.Render();
        }

        public Card GetTarget()
        {
            if (DefaultTarget)
                return DefaultTarget;
            if (Side == 0 && CombatControl.Main.FriendlyCards.Count > Index)
                return CombatControl.Main.FriendlyCards[Index];
            else if (Side == 1 && CombatControl.Main.EnemyCards.Count > Index)
                return CombatControl.Main.EnemyCards[Index];
            return null;
        }

        public void MainRender()
        {
            if (!GetTarget())
                return;

            // Temp
            if (GetTarget().CombatActive())
                SetActive(true);
            else
                SetActive(false);

            if (NameText)
            {
                if (LifeRenderDirection == 1)
                    NameText.text = GetTarget().GetName() + " " + Mathf.RoundToInt(GetTarget().GetLife()) + "/" + Mathf.RoundToInt(GetTarget().GetMaxLife());
                else if (LifeRenderDirection == -1)
                    NameText.text = Mathf.RoundToInt(GetTarget().GetLife()) + "/" + Mathf.RoundToInt(GetTarget().GetMaxLife()) + " " + GetTarget().GetName();
                else
                    NameText.text = GetTarget().GetName();
            }

            if (GetTarget().GetLife() <= 0)
            {
                if (LifeText)
                    LifeText.text = "";
                if (LifeTextII)
                    LifeTextII.text = "";
            }
            else if (GetTarget().GetLife() / GetTarget().GetMaxLife() >= 0.05f)
            {
                if (LifeText)
                    LifeText.text = ((int)GetTarget().GetLife()).ToString();
                if (LifeTextII)
                    LifeTextII.text = "";
            }
            else
            {
                if (LifeText)
                    LifeText.text = "";
                if (LifeTextII)
                    LifeTextII.text = ((int)GetTarget().GetLife()).ToString();
            }

            float m = GetTarget().PassValue("Mana");
            if (m <= 0)
            {
                if (ManaText)
                    ManaText.text = "";
                if (ManaTextII)
                    ManaTextII.text = "";
            }
            else if (m >= 0.05f)
            {
                if (ManaText)
                    ManaText.text = (int)(m * 100) + "%";
                if (ManaTextII)
                    ManaTextII.text = "";
            }
            else
            {
                if (ManaText)
                    ManaText.text = "";
                if (ManaTextII)
                    ManaTextII.text = (int)(m * 100) + "%";
            }

            int a = Mathf.RoundToInt(GetTarget().GetLife() / GetTarget().GetMaxLife() * 25);
            string s = "";
            for (int i = 0; i < a; i++)
                s += "-";
            //LifeBar.text = s;

            if (LifeBar)
                LifeBar.Render(GetTarget().GetLife() / GetTarget().GetMaxLife());
            if (ManaBar)
                ManaBar.Render(GetTarget().PassValue("Mana"));
            if (DamageText)
                DamageText.text = GetTarget().GetBaseDamage().ToString();
            if (AttackSpeedText)
                AttackSpeedText.text = GetTarget().PassValue("AttackSpeed", 1).ToString();
            if (RecoverySpeedText)
                RecoverySpeedText.text = GetTarget().PassValue("ManaRecovery", 1).ToString();
        }

        public void SetActive(bool Value)
        {
            if (AnimBase)
                AnimBase.SetActive(Value);
        }
    }
}